using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.Seguridad
{
    public partial class frmPermisosAcceso : Form
    {
        private static frmPermisosAcceso _frm = null;
        private GyCAP.Data.dsSeguridad dsSeguridad = new GyCAP.Data.dsSeguridad();
        private DataView dvUsuario, dvMenu, dvMenuUsuario;
        public static readonly int estadoInicialNuevo = 1; //Indica que debe iniciar como nuevo
        public static readonly int estadoInicialConsultar = 2; //Indica que debe inicial como buscar
        private Boolean ejecutaAccion = true;

        public frmPermisosAcceso()
        {
            InitializeComponent();

            //CARGA DE COMBOS
            try
            {
                BLL.UsuarioBLL.ObtenerTodos(dsSeguridad.USUARIOS);
                BLL.MenuBLL.ObtenerTodos(dsSeguridad.MENU); 
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex) { Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text, GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Inicio); }

            //Dataviews
            dvMenu = new DataView(dsSeguridad.MENU);
            dvMenu.Sort = "MNU_ORDEN ASC";

            dvUsuario = new DataView(dsSeguridad.USUARIOS);
            dvUsuario.Sort = "U_NOMBRE ASC";

            //Combos
            cboUsuarios.SetDatos(dvUsuario,  "U_CODIGO", "U_NOMBRE", "Seleccione...", true);

            setArbol();

        }

        #region Servicios

        //Método para evitar la creación de más de una pantalla
        public static frmPermisosAcceso Instancia
        {
            get
            {
                if (_frm == null || _frm.IsDisposed)
                {
                    _frm = new frmPermisosAcceso();
                }
                else
                {
                    _frm.BringToFront();
                }
                return _frm;
            }
            set
            {
                _frm = value;
            }
        }

        private void setArbol() 
        {
            int i,j;
            TreeNode node = new TreeNode ("Menú");
            treeMenu.Nodes.Add(node);
            i = 0;
            j = 0;
            try
            {
                foreach (DataRowView  row in dvMenu)
                {
                    if (row["MNU_PADRE"].ToString() == string.Empty)
                    {
                        node.Nodes.Add(row["MNU_NOMBRE"].ToString(), row["MNU_DESCRIPCION"].ToString());
                        node.Nodes[i].Tag = row["MNU_CODIGO"].ToString();
                        i += 1;
                        j = 0;
                    }
                    else
                    {
                        node.Nodes[i - 1].Nodes.Add(row["MNU_NOMBRE"].ToString(), row["MNU_DESCRIPCION"].ToString());
                        node.Nodes[i - 1].Nodes[j].Tag = row["MNU_CODIGO"].ToString();
                        j  += 1;
                    }
                }
                treeMenu.Nodes[0].ExpandAll();
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex) { Entidades.Mensajes.MensajesABM.MsjExcepcion(ex.Message, this.Text,  GyCAP.Entidades.Mensajes.MensajesABM.Operaciones.Inicio); }

        }

        private void RecorrerNodos(TreeNode treeNode, Boolean check)
        {
            try
            {
                //Si el nodo que recibimos tiene hijos se recorrerá
                //para luego verificar si esta o no checado
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    tn.Checked = check;
                    //Ahora hago verificacion a los hijos del nodo actual
                    //Esta iteración no acabara hasta llegar al ultimo nodo principal
                    if (tn.Nodes.Count > 0)
                    {
                        RecorrerNodos(tn, check);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RecorrerNodosPermisos(TreeNode treeNode)
        {
            try
            {
                if (treeNode.Nodes.Count > 0)
                {
                    //Si el nodo que recibimos tiene hijos se recorrerá
                    //para luego verificar si esta o no checado
                    foreach (TreeNode tn in treeNode.Nodes)
                    {
                        foreach (DataRowView row in dvMenuUsuario)
                        {
                            if (tn.Tag.ToString() == row["MNU_CODIGO"].ToString())
                            {
                                tn.Checked = true;
                            }
                        }
                        //Ahora hago verificacion a los hijos del nodo actual
                        //Esta iteración no acabara hasta llegar al ultimo nodo principal
                        if (tn.Nodes.Count > 0)
                        {
                            RecorrerNodosPermisos(tn);
                        }

                    }
                }
                if (treeNode.Tag != null)
                {
                    foreach (DataRowView row in dvMenuUsuario)
                    {
                        if (treeNode.Tag == row["MNU_CODIGO"])
                        {
                            treeNode.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RecorrerNodosGuardar(TreeNode treeNode, Entidades.Usuario usuario)
        {
            Entidades.MenuUsuario mu = new GyCAP.Entidades.MenuUsuario();
            Entidades.Menu m = new GyCAP.Entidades.Menu();

            //Insertar
            if (treeNode.Checked == true && treeNode.Tag != null)
            {
                mu.Usuario = usuario;
                m.Codigo = int.Parse(treeNode.Tag.ToString());
                mu.Menu = m;
                BLL.MenuUsuarioBLL.Insertar(mu);
            }

            if (treeNode.Nodes.Count > 0)
            {
                foreach (TreeNode tn in treeNode.Nodes)
                {
                    RecorrerNodosGuardar(tn, usuario);
                }

            }

        }

        private void setPermisosUsuarios()
        {
            ejecutaAccion = false;
            RecorrerNodosPermisos(treeMenu.Nodes[0]);
            ejecutaAccion = true;
        }

        private void Insertar() 
        {
            int usuario;
            Entidades.Usuario u = new GyCAP.Entidades.Usuario();

            if (cboUsuarios.Text == "Seleccione...")
            {
                MessageBox.Show("Seleccione un Usuario.", "Seleccionar...",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            usuario = cboUsuarios.GetSelectedValueInt();

            u.Codigo = usuario;

            //Borrar todos los permisos del usuario
            BLL.MenuUsuarioBLL.Eliminar(usuario);

            //Insertar
            RecorrerNodosGuardar(treeMenu.Nodes[0], u); 
        }

        #endregion Servicios

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void treeMenu_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (ejecutaAccion== true)
            {
                if (e.Node.Parent != null)
                    e.Node.Parent.Checked = true;    
            }
            
        }

        private void cboUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int usuario;

            //Deschequeo todo
            foreach (TreeNode node in treeMenu.Nodes)
            {
                node.Checked = false;
                if (node.Nodes.Count > 0)
                    RecorrerNodos(node, false);
            }

            if (cboUsuarios.Text == "Seleccione...")
                return;

            dsSeguridad.MENU_USUARIOS.Clear();

            usuario = cboUsuarios.GetSelectedValueInt();
            BLL.MenuUsuarioBLL.ObtenerTodos(dsSeguridad);

            dvMenuUsuario = new DataView(dsSeguridad.MENU_USUARIOS);

            dvMenuUsuario.RowFilter = "U_CODIGO = " + usuario; 
            setPermisosUsuarios();

        }

        private void treeMenu_Click(object sender, EventArgs e)
        {
            //TreeNode node;
            //if (treeMenu.SelectedNode != null)
            //{
            //    node = treeMenu.SelectedNode;
            //    if (node.Checked)
            //        node.Checked = false;
            //    else
            //        node.Checked = true;
            //}
            //else
            //    return;

            //foreach (TreeNode hijo in node.Nodes)
            //{
            //    hijo.Checked = node.Checked;
            //}
       
        }

        private void cboUsuarios_Click(object sender, EventArgs e)
        {
            //int usuario;

            //if (cboUsuarios.GetSelectedValue() == "Seleccione...")
            //    return;

            //usuario = cboUsuarios.GetSelectedValueInt();
            //BLL.MenuUsuarioBLL.ObtenerTodos(usuario, dsSeguridad);

            //dvMenuUsuario = new DataView(dsSeguridad.MENU_USUARIOS);

            //setPermisosUsuarios();
        }

        private void cboUsuarios_SelectedValueChanged(object sender, EventArgs e)
        {
            //int usuario;

            //if (cboUsuarios.Text == "Seleccione...")
            //    return;

            //usuario = cboUsuarios.GetSelectedValueInt();
            //BLL.MenuUsuarioBLL.ObtenerTodos(usuario, dsSeguridad);

            //dvMenuUsuario = new DataView(dsSeguridad.MENU_USUARIOS);

            //setPermisosUsuarios();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            int usuario;

            if (cboUsuarios.Text == "Seleccione...")
            {
                MessageBox.Show("Seleccione un Usuario.", "Seleccionar...",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            usuario = cboUsuarios.GetSelectedValueInt();
            Insertar();

            MessageBox.Show("Los permisos fueron actualizados con éxito.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cboUsuarios.Text == "Seleccione...")
            {
                MessageBox.Show("Seleccione un Usuario.", "Seleccionar...",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Insertar();
            btnSalir.PerformClick();
        }

        private void treeMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //if (e.Node.Parent != null) 
            //{
            //    if(e.Node.Checked == true)
            //        e.Node.Parent.Checked = true;        
            //}
                
        }
    }
}
