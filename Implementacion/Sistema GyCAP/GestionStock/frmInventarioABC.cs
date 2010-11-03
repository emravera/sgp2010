using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.UI.GestionStock
{
    public partial class frmInventarioABC : Form
    {
        private static frmInventarioABC _frmInventarioABC = null;
        private enum estadoUI { inicio, CargaDetalle, generaHistorico, generadoABC };
        private DataView dvComboAño, dvComboAñoHistorico, dvComboCocinas, dvListaModelos, dvListaMP;
        private static estadoUI estadoActual;
        private Data.dsInventarioABC dsInventarioABC = new GyCAP.Data.dsInventarioABC();
        private static int codigoModelo = 0; 
        private static int codigoMatPrima=0;
        //Defino una lista generica
        private static List<Entidades.MateriaPrimaABC> materiasPrimas = new List<GyCAP.Entidades.MateriaPrimaABC>();
       
        
        public frmInventarioABC()
        {
            InitializeComponent();

            //Ponemos que las columnas no se autogeneren
            dgvModelos.AutoGenerateColumns = false;
            dgvMP.AutoGenerateColumns = false;

            //*********************************CREACION GRILLAS *********************************
            //Grilla de Modelos
            //Agregamos la columnas
            
            dgvModelos.Columns.Add("CODIGO_MODELO", "Codigo");
            dgvModelos.Columns.Add("CODIGO_MODELO_PRODUCIDO", "Modelo");
            dgvModelos.Columns.Add("MODELO_PORCENTAJE", "Porcentaje");
            dgvModelos.Columns.Add("MODELO_CANTIDAD", "Cantidad");

            //Seteamos el modo de tamaño de las columnas
            dgvModelos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dgvModelos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvModelos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvModelos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvModelos.Columns["CODIGO_MODELO_PRODUCIDO"].DataPropertyName = "CODIGO_MODELO_PRODUCIDO";
            dgvModelos.Columns["CODIGO_MODELO"].DataPropertyName = "CODIGO_MODELO";
            dgvModelos.Columns["MODELO_PORCENTAJE"].DataPropertyName = "MODELO_PORCENTAJE";
            dgvModelos.Columns["MODELO_CANTIDAD"].DataPropertyName = "MODELO_CANTIDAD";

            //Indicamos la alineacion de los campos
            dgvModelos.Columns["MODELO_PORCENTAJE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Creamos el dataview y lo asignamos a la grilla
            dvListaModelos = new DataView(dsInventarioABC.MODELOS_PRODUCIDOS);
            dgvModelos.DataSource = dvListaModelos;

            //Grilla de Materias Primas ABC
            //Agregamos la columnas
            dgvMP.Columns.Add("CODIGO_MATERIA_PRIMA_ABC", "Código");
            dgvMP.Columns.Add("CODIGO_MATERIA_PRIMA", "MP");
            dgvMP.Columns.Add("CANTIDAD_ANUAL", "Cant.Anual");
            dgvMP.Columns.Add("PRECIO_UNIDAD", "Precio");
            dgvMP.Columns.Add("CANTIDAD_INVERSION", "$ Inversion");
            dgvMP.Columns.Add("PORCENTAJE_INVERSION", "% Inv.");
            dgvMP.Columns.Add("CATEGORIA_ABC", "Cat.");

            //Seteamos el modo de tamaño de las columnas
            dgvMP.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMP.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            //Indicamos de dónde van a sacar los datos cada columna, el nombre debe ser exacto al de la DB
            dgvMP.Columns["CODIGO_MATERIA_PRIMA_ABC"].DataPropertyName = "CODIGO_MATERIA_PRIMA_ABC";
            dgvMP.Columns["CODIGO_MATERIA_PRIMA"].DataPropertyName = "CODIGO_MATERIA_PRIMA";
            dgvMP.Columns["CANTIDAD_ANUAL"].DataPropertyName = "CANTIDAD_ANUAL";
            dgvMP.Columns["PRECIO_UNIDAD"].DataPropertyName = "PRECIO_UNIDAD";
            dgvMP.Columns["CANTIDAD_INVERSION"].DataPropertyName = "CANTIDAD_INVERSION";
            dgvMP.Columns["PORCENTAJE_INVERSION"].DataPropertyName = "PORCENTAJE_INVERSION";
            dgvMP.Columns["CATEGORIA_ABC"].DataPropertyName = "CATEGORIA_ABC";

            //Indicamos la alineacion de los numeros
            dgvMP.Columns["CODIGO_MATERIA_PRIMA_ABC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMP.Columns["CANTIDAD_ANUAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMP.Columns["PRECIO_UNIDAD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMP.Columns["CANTIDAD_INVERSION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMP.Columns["PORCENTAJE_INVERSION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
           


            //Creamos el dataview y lo asignamos a la grilla
            dvListaMP = new DataView(dsInventarioABC.MATERIAS_PRIMAS_ABC);
            dgvMP.DataSource = dvListaMP;


            //Traemos los datos para llenar Datatables 
            //Traigo los datos de MP
            BLL.MateriaPrimaBLL.ObtenerMP(dsInventarioABC.MATERIAS_PRIMAS);

            //Traigo los datos de Modelos Cocinas
            BLL.CocinaBLL.ObtenerCocinas(dsInventarioABC.COCINAS);

            //Planes Anuales
            BLL.PlanAnualBLL.ObtenerTodos(dsInventarioABC.PLANES_ANUALES);

            //CARGA DE COMBOS
            //Cargo el combo de datos del plan anual
            dvComboAño = new DataView(dsInventarioABC.PLANES_ANUALES);
            cbAñoInventario.SetDatos(dvComboAño, "pan_codigo", "pan_anio", "Seleccione", false);

            //Cargo el combo del plan anual historico
            dvComboAñoHistorico = new DataView(dsInventarioABC.PLANES_ANUALES);
            cbAñoHistorico.SetDatos(dvComboAñoHistorico, "pan_codigo", "pan_anio", "Seleccione", false);

            //Cargo el Combo de Cocinas
            dvComboCocinas = new DataView(dsInventarioABC.COCINAS);
            cbCocinas.SetDatos(dvComboCocinas, "coc_codigo", "coc_codigo_producto", "Seleccione", false);
            
            //Se ponen los decimales al porcentaje
            numPorcentaje.DecimalPlaces = 2;

            //Seteamos la interface
            SetInterface(estadoUI.inicio);
        }


        #region Servicios
        
        public static frmInventarioABC Instancia
        {
            get
            {
                if (_frmInventarioABC == null || _frmInventarioABC.IsDisposed)
                {
                    _frmInventarioABC = new frmInventarioABC();
                }
                else
                {
                    _frmInventarioABC.BringToFront();
                }
                return _frmInventarioABC;
            }
            set
            {
                _frmInventarioABC = value;
            }
        }

        private void SetInterface(estadoUI estado)
        {
            switch (estado)
            {
                case estadoUI.inicio:
                    //Groupbox
                    gbDatosPrincipales.Visible = true;
                    gbDatosCocinas.Visible = false;
                    gbMateriasPrimas.Visible = false;

                    //Otros controles
                    rbNuevo.Checked = true;
                    cbAñoHistorico.Visible = false;
                    txtCantAnual.ReadOnly = true;

                    //Escondo las columnas que no quiero que se vean
                    dgvModelos.Columns["CODIGO_MODELO"].Visible = false;

                    break;

                case estadoUI.CargaDetalle:
                    gbDatosPrincipales.Enabled=false;
                    gbDatosCocinas.Visible = true;
                    gbMateriasPrimas.Visible = false;

                    //Escondo las columnas que no quiero que se vean
                    dgvModelos.Columns["CODIGO_MODELO"].Visible = false;
                    break;
                case estadoUI.generaHistorico:
                    gbDatosPrincipales.Enabled = false;
                    gbDatosCocinas.Visible = true;
                    gbMateriasPrimas.Visible = false;
                   
                    //Escondo las columnas que no quiero que se vean
                    dgvModelos.Columns["CODIGO_MODELO"].Visible = false;
                    break;
                case estadoUI.generadoABC:
                    gbDatosPrincipales.Enabled = false;
                    gbDatosCocinas.Visible = true;
                    gbMateriasPrimas.Visible = true;

                    gbMateriasPrimas.Enabled = true;
                    gbDatosCocinas.Enabled = false;

                    //Escondo las columnas que no quiero que se vean
                    dgvMP.Columns["CODIGO_MATERIA_PRIMA_ABC"].Visible = false;
                    break;

            }
        }
        private void rbNuevo_CheckedChanged(object sender, EventArgs e)
        {
            cbAñoHistorico.Visible = false;
            cbAñoHistorico.SetSelectedIndex(-1);
        }

        private void rbHistorico_CheckedChanged(object sender, EventArgs e)
        {
            cbAñoHistorico.Visible = true;
        }

        private string ValidarDetalle()
        {
            string msjerror = string.Empty;

            if (cbCocinas.SelectedIndex == -1) msjerror = msjerror + "-Debe seleccionar un modelo de cocina\n";
            if (numPorcentaje.Value == 0) msjerror = msjerror + "-El porcentaje debe ser mayor a cero\n";
            
            //Validamos que no se quiera agregar un modelo que ya está en el dataset
            foreach (Data.dsInventarioABC.MODELOS_PRODUCIDOSRow row in (Data.dsInventarioABC.MODELOS_PRODUCIDOSRow[])dsInventarioABC.MODELOS_PRODUCIDOS.Select(null, null, System.Data.DataViewRowState.Added))
            {
                if (row["CODIGO_MODELO_PRODUCIDO"].ToString() != string.Empty)
                {
                    if (row["CODIGO_MODELO_PRODUCIDO"].ToString() == Convert.ToString(cbCocinas.GetSelectedValue()))
                    {
                        msjerror = msjerror + "-El modelo de cocina que intenta agregar ya se encuentra\n";
                    }
                }
            }

            if (msjerror.Length > 0)
            {
                msjerror = "Los errores encontrados son:\n" + msjerror;
            }
            return msjerror;
        }

        private string ValidarABC()
        {
            string msjerror = string.Empty;

            int Porcentaje=0;

            //Validamos que no se quiera agregar un modelo que ya está en el dataset
            foreach (Data.dsInventarioABC.MODELOS_PRODUCIDOSRow row in dsInventarioABC.MODELOS_PRODUCIDOS.Rows)
            {
                if (row["MODELO_PORCENTAJE"].ToString() != string.Empty)
                {
                    Porcentaje = Porcentaje + Convert.ToInt32(row["MODELO_PORCENTAJE"]);
                }
            }
            
            if (Porcentaje !=100)
            {
                msjerror = msjerror + "-El porcentaje debe ser igual 100\n";
            }

            if (msjerror.Length > 0)
            {
                msjerror = "Los errores encontrados son:\n" + msjerror;
            }

            return msjerror;
        }

        #endregion

        private void btnGenerarInventario_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbAñoInventario.GetSelectedIndex() != -1)
                {
                    if (rbNuevo.Checked == true)
                    {
                        //Se busca el año del que hay que traer 
                        int codigoAnio = Convert.ToInt32(cbAñoInventario.GetSelectedValue());

                        //Se trae el el total a producir en ese año desde la base de datos
                        txtCantAnual.Text = BLL.StockMateriaPrimaBLL.ObtenerTotalAnual(codigoAnio).ToString();

                        //Seteamos la interface
                        SetInterface(estadoUI.CargaDetalle);
                    }
                    else
                    {
                        //Se buscan los porcentajes de cocinas producidas del año historico

                        //Obtengo el plan anual
                        int año = Convert.ToInt32(cbAñoInventario.GetSelectedValue());


                        //Se trae el el total a producir en ese año desde la base de datos
                        txtCantAnual.Text = BLL.StockMateriaPrimaBLL.ObtenerTotalAnual(año).ToString();

                        //Se obtiene el año histórico
                        int añoHistorico = Convert.ToInt32(cbAñoHistorico.GetSelectedValue());

                        //Defino la matriz donde voy a guardar los datos
                        int cantidadModelos = Convert.ToInt32(dsInventarioABC.COCINAS.Rows.Count);
                        decimal[,] modelos = new decimal[cantidadModelos, 2];
                        int cont = 0; decimal sum = 0;

                        foreach (Data.dsInventarioABC.COCINASRow cocina in dsInventarioABC.COCINAS.Rows)
                        {
                            //Obtengo el codigo de la cocina
                            int codigoModelo = Convert.ToInt32(cocina["coc_codigo"]);
                            modelos[cont, 0] = codigoModelo;
                            //Obtengo de ese modelo la cantidad de cocinas producidas en ese año
                            object salida = BLL.StockMateriaPrimaBLL.ObtenerTotalModelo(añoHistorico, codigoModelo);
                            if (salida != DBNull.Value)
                            {
                                modelos[cont, 1] = Convert.ToInt32(salida);
                            }
                            else
                            {
                                modelos[cont, 1] = 0;
                            }
                            //Calculo el total
                            sum += modelos[cont, 1];
                            //Sumo uno mas al contador
                            cont += 1;
                        }
                        if (sum == 0)
                        {
                            //Le calculo los porcentajes a cada elemento de la matriz
                            for (int i = 0; i < cantidadModelos; i++)
                            {
                                modelos[i, 1] = (modelos[i, 1] / sum) * 100;
                            }

                            //Asigno los valores a la datatable de modelos
                            for (int j = 0; j < cantidadModelos; j++)
                            {
                                if (modelos[j, 1] > 0)
                                {
                                    //Se crea una fila
                                    Data.dsInventarioABC.MODELOS_PRODUCIDOSRow row = dsInventarioABC.MODELOS_PRODUCIDOS.NewMODELOS_PRODUCIDOSRow();

                                    //Se comienza a editar la fila
                                    row.BeginEdit();
                                    codigoModelo = codigoModelo + 1;
                                    row.CODIGO_MODELO = codigoModelo;
                                    row.CODIGO_MODELO_PRODUCIDO = modelos[j, 0];
                                    row.MODELO_PORCENTAJE = Math.Round(modelos[j, 1], 2);
                                    row.MODELO_CANTIDAD = Math.Round(((modelos[j, 1] / 100) * Convert.ToInt32(txtCantAnual.Text)), 0);
                                    row.EndEdit();

                                    //Agregamos la Línea
                                    dsInventarioABC.MODELOS_PRODUCIDOS.AddMODELOS_PRODUCIDOSRow(row);
                                }
                            }

                            //Seteamos la interface
                            SetInterface(estadoUI.generaHistorico);
                        }
                        else
                        {
                            MessageBox.Show("No existen datos para calcular el histórico", "Error: Inventario ABC - Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un año para el Inventario ABC" , "Error: Inventario ABC - Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Inventario ABC - Carga Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                string validacion = ValidarDetalle();

                if (validacion == string.Empty)
                {

                    //Se crea una fila
                    Data.dsInventarioABC.MODELOS_PRODUCIDOSRow row = dsInventarioABC.MODELOS_PRODUCIDOS.NewMODELOS_PRODUCIDOSRow();

                    //Se comienza a editar la fila
                    row.BeginEdit();
                    codigoModelo = codigoModelo + 1;
                    row.CODIGO_MODELO = codigoModelo;
                    row.CODIGO_MODELO_PRODUCIDO = Convert.ToInt32(cbCocinas.GetSelectedValue());
                    row.MODELO_PORCENTAJE =Math.Round(Convert.ToDecimal(numPorcentaje.Value),2);
                    row.MODELO_CANTIDAD = Math.Round(((Convert.ToDecimal(numPorcentaje.Value) / 100) * Convert.ToInt32(txtCantAnual.Text)),0);
                    row.EndEdit();

                    //Agregamos la Línea
                    dsInventarioABC.MODELOS_PRODUCIDOS.AddMODELOS_PRODUCIDOSRow(row);

                    //Volvemos al estado inicial
                    cbCocinas.SetSelectedIndex(-1);
                    numPorcentaje.Value = 0;
                }
                else
                {
                    MessageBox.Show(validacion, "Error: Inventario ABC - Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Entidades.Excepciones.BaseDeDatosException ex)
            {
                MessageBox.Show(ex.Message, "Error: Inventario ABC - Carga Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvModelos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string nombre;

                switch (dgvModelos.Columns[e.ColumnIndex].Name)
                {
                    case "CODIGO_MODELO_PRODUCIDO":
                        nombre = dsInventarioABC.COCINAS.FindByCOC_CODIGO(Convert.ToInt32(e.Value)).COC_CODIGO_PRODUCTO;
                        e.Value = nombre;
                        break;
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            dsInventarioABC.MODELOS_PRODUCIDOS.Clear();
            gbDatosPrincipales.Enabled = true;
            gbDatosCocinas.Visible = false;
            gbMateriasPrimas.Visible = false;
            txtCantAnual.Text = "";            
        }

        private void btnCalcularABC_Click(object sender, EventArgs e)
        {
            string validacion = ValidarABC();
            if (validacion == string.Empty)
            {
                //Limpiamos el Dataset de Materias Primas
                dsInventarioABC.MATERIAS_PRIMAS_ABC.Clear();

                //Contamos la cantidad de modelos
                int cantidadModelos =Convert.ToInt32(dsInventarioABC.MODELOS_PRODUCIDOS.Rows.Count);

                //Defino las materias primas
                decimal[,] mpModelo = new decimal[1,1];
                decimal[,] mpCostos ;

                              
                //Buscamos las materias primas para cada modelo de cocina
                foreach (Data.dsInventarioABC.MODELOS_PRODUCIDOSRow row in dsInventarioABC.MODELOS_PRODUCIDOS.Rows)
                {
                    //Busco el modelo de cocina
                    int modeloCocina = Convert.ToInt32(row["CODIGO_MODELO_PRODUCIDO"]);

                    //Busco la cantidad de ese modelo a producir
                    decimal cantidadPrducir = Convert.ToDecimal(row["MODELO_CANTIDAD"]);

                    
                    //Obtengo las materias primas de ese modelo
                    mpModelo = BLL.EstructuraBLL.ObtenerMateriasPrimasYCantidades(modeloCocina);
                    
                    //Calculo las cantidades para todo el año
                    for (int j = 0; j < (mpModelo.Length/2); j++)
                    {
                        mpModelo[j,1] = (mpModelo[j,1] * cantidadPrducir);
                    }

                    //Paso las cantidades a Precios
                    mpCostos = new decimal[(mpModelo.Length / mpModelo.Rank), 2];
                    
                    for (int i = 0; i < (mpModelo.Length/2); i++)
                    {
                        //Ontengo el Codigo de la MP
                        decimal codigoMP = mpModelo[i,0];  

                        //Obtengo el precio de esa materia prima
                        decimal precioMP = BLL.MateriaPrimaBLL.ObtenerPrecioMP(codigoMP);

                        //Asigno el codigo de MP y el costo total anual
                        mpCostos[i,0] = codigoMP;
                        mpCostos[i,1] = (mpModelo[i,1] * precioMP);                        
                    }


                    //Me crea una matriz unica con el costo y la cantidad
                    decimal[,] mpUnica = new decimal[(mpModelo.Length / mpModelo.Rank), 3];

                    //Se Definen las Columnas: 0-codigoMP, 1-CantidadMPAnio, 2-CostoMPAnio
                    for (int j = 0; j < (mpModelo.Length / mpModelo.Rank); j++)
                    {
                        mpUnica[j, 0] = mpModelo[j, 0];
                        mpUnica[j, 1] = mpModelo[j, 1];
                        mpUnica[j, 2] = mpCostos[j, 1];
                    }

                    //Se añade la matriz unificada a la Lista
                    ActualizarLista(mpUnica);
                }

                //Operaciones para calcular los porcentajes, ordenarlos y asignarle las clases
                calcularPorcentajes();

                //Se ordena la lista por los porcentajes
                materiasPrimas = materiasPrimas.OrderBy(x => x.PorcentajeMP).ToList();
                materiasPrimas.Reverse();

                //Se caculan las clases a partir de los valores obtenidos
                determinarABC();

                //Se pasa la lista generica al DataTable  de materias primas ABC
                generarDataTable();

                //Se muestra el groupbox donde esta la lista de clasificacion
                SetInterface(estadoUI.generadoABC);

            }
            else
            {
                MessageBox.Show(validacion, "Error: Inventario ABC - Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        //Funcion que maneja la lista de MP
        private void ActualizarLista(decimal[,] matrizUnica)
        {
            for (int i = 0; i < (matrizUnica.Length / (matrizUnica.Rank + 1)); i++)
            {
                //obtengo el codigo de MP
                decimal codigoMP = matrizUnica[i,0];

                //Pregunto si existe la materia prima en la lista
                if (existeMPLista(codigoMP,materiasPrimas))
                {
                    //Se actualizan los valores de la misma
                    foreach (Entidades.MateriaPrimaABC mp in materiasPrimas)
                    {
                        //Si es la materia prima le sumo la cantidad y el capital
                        if (mp.CodigoMP == codigoMP)
                        {
                            mp.CantidadMP += matrizUnica[i, 1];
                            mp.Inversion += matrizUnica[i, 2];
                        }
                    }
                }
                else
                {
                    //Si no existe creo el objeto y lo agrego a la lista
                    Entidades.MateriaPrimaABC mp = new GyCAP.Entidades.MateriaPrimaABC();

                    mp.CodigoMP =Convert.ToInt32(codigoMP);
                    mp.CantidadMP = matrizUnica[i, 1];
                    mp.Inversion = matrizUnica[i, 2];
                    mp.PrecioMP = BLL.MateriaPrimaBLL.ObtenerPrecioMP(codigoMP);

                    //Lo agrego a la lista generica
                    materiasPrimas.Add(mp);
                }
            }

        }

        //Funcion que determina si ya existe una determinada MP en la Lista
        private bool existeMPLista(decimal codigoMP, List<Entidades.MateriaPrimaABC> materiasPrimas)
        {
            foreach (Entidades.MateriaPrimaABC MP in materiasPrimas)
            {
                if (MP.CodigoMP == codigoMP)
                {
                    return true;
                }
            }
            return false;
        }

        //Funcion que determina los porcentajes que representa la inversion de cada materia prima
        private void calcularPorcentajes()
        {
            //Calculo el total de inversion necesario
            decimal totalInversion = 0;

            foreach (Entidades.MateriaPrimaABC MP in materiasPrimas)
            {
                totalInversion += MP.Inversion;                                
            }

            //Con el total ahora calculo el porcentaje para cada una de las materias primas
            foreach (Entidades.MateriaPrimaABC mp in materiasPrimas)
            {
                mp.PorcentajeMP =Math.Round(((mp.Inversion / totalInversion) *100),2);
            }

        }
        //Funcion que determina la Clase ABC de cada materia Prima
        private void determinarABC()
        {
            decimal acumulador = 0;

            foreach (Entidades.MateriaPrimaABC MP in materiasPrimas)
            {
                acumulador += MP.PorcentajeMP;
                
                //Clasifico las materias primas en clases
                if(acumulador <= 80)
                {
                    MP.ClaseMP= "A";
                }
                if (acumulador >80 && acumulador <= 95)
                {
                    MP.ClaseMP = "B";
                }
                if (acumulador > 95 && acumulador <= 100)
                {
                    MP.ClaseMP = "C";
                }
            }            
        }

        //Funcion que pasa la lista generica al DataTable de materias primas
        private void generarDataTable()
        {
            foreach (Entidades.MateriaPrimaABC MP in materiasPrimas)
            {
                //Se crea una fila y se la agrega al dataset
                Data.dsInventarioABC.MATERIAS_PRIMAS_ABCRow row = dsInventarioABC.MATERIAS_PRIMAS_ABC.NewMATERIAS_PRIMAS_ABCRow();

                codigoMatPrima += 1;
                row.CODIGO_MATERIA_PRIMA_ABC = codigoMatPrima;
                row.CODIGO_MATERIA_PRIMA = MP.CodigoMP;
                row.CANTIDAD_ANUAL =Math.Round(MP.CantidadMP,2);
                row.PRECIO_UNIDAD = MP.PrecioMP;
                row.CANTIDAD_INVERSION=Math.Round(MP.Inversion,2);
                row.PORCENTAJE_INVERSION = MP.PorcentajeMP;
                row.CATEGORIA_ABC = MP.ClaseMP;

                dsInventarioABC.MATERIAS_PRIMAS_ABC.AddMATERIAS_PRIMAS_ABCRow(row);                
            }
            
        }

        private void dgvMP_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                string nombre;

                switch (dgvMP.Columns[e.ColumnIndex].Name)
                {
                    case "CODIGO_MATERIA_PRIMA":
                        nombre = dsInventarioABC.MATERIAS_PRIMAS.FindByMP_CODIGO(Convert.ToInt32(e.Value)).MP_NOMBRE;
                        e.Value = nombre;
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gbDatosCocinas.Enabled = true;
            gbMateriasPrimas.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvModelos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                int codigo = Convert.ToInt32(dvListaModelos[dgvModelos.SelectedRows[0].Index]["codigo_modelo"]);
                
                //Elimino del dataset
                dsInventarioABC.MODELOS_PRODUCIDOS.FindByCODIGO_MODELO(codigo).Delete();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnSumar_Click(object sender, EventArgs e)
        {
            if (dgvModelos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Actualizo el porcentaje
                int codigo = Convert.ToInt32(dvListaModelos[dgvModelos.SelectedRows[0].Index]["codigo_modelo"]);
                dsInventarioABC.MODELOS_PRODUCIDOS.FindByCODIGO_MODELO(codigo).MODELO_PORCENTAJE +=Convert.ToDecimal(0.01);
                
                //Actualizo la cantidad
                decimal porcentaje = Convert.ToDecimal(dvListaModelos[dgvModelos.SelectedRows[0].Index]["modelo_porcentaje"]);
                decimal cantidad = Convert.ToDecimal(txtCantAnual.Text);
                dsInventarioABC.MODELOS_PRODUCIDOS.FindByCODIGO_MODELO(codigo).MODELO_CANTIDAD =Math.Round(((porcentaje/100)*cantidad),0);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestar_Click(object sender, EventArgs e)
        {
            if (dgvModelos.Rows.GetRowCount(DataGridViewElementStates.Selected) != 0)
            {
                //Actualizo el porcentaje
                int codigo = Convert.ToInt32(dvListaModelos[dgvModelos.SelectedRows[0].Index]["codigo_modelo"]);
                dsInventarioABC.MODELOS_PRODUCIDOS.FindByCODIGO_MODELO(codigo).MODELO_PORCENTAJE -=Convert.ToDecimal(0.01);

                //Actualizo la cantidad
                decimal porcentaje = Convert.ToDecimal(dvListaModelos[dgvModelos.SelectedRows[0].Index]["modelo_porcentaje"]);
                decimal cantidad = Convert.ToDecimal(txtCantAnual.Text);
                dsInventarioABC.MODELOS_PRODUCIDOS.FindByCODIGO_MODELO(codigo).MODELO_CANTIDAD = Math.Round(((porcentaje / 100) * cantidad), 0);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un modelo de cocina de la lista.", "Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       



    }
}
