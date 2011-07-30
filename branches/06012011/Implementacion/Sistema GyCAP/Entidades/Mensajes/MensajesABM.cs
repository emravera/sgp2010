using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GyCAP.Entidades.Mensajes
{
    public class ItemValidacion
    {
        private MensajesABM.Validaciones tipoValidacion;

        public MensajesABM.Validaciones TipoValidacion
        {
            get { return tipoValidacion; }
            set { tipoValidacion = value; }
        }
        private string dato;

        public string Dato
        {
            get { return dato; }
            set { dato = value; }
        }

        public ItemValidacion(MensajesABM.Validaciones validacion, string datoValidar)
        {
            tipoValidacion = validacion;
            dato = datoValidar;
        }
    }
    
    public class MensajesABM
    {
        //Los siguientes son metodos que muestran mensajes estandares para los formularios de ABM


        //***************************************************************************************
        //                                  Enumeraciones Publicas
        //***************************************************************************************
        public enum Operaciones { Inicio, Búsqueda, Eliminación, Guardado, Modificación }
        public enum Generos { Femenino, Masculino }
        public enum Validaciones { Seleccion, CompletarDatos, SoloEspacios, Logica }

        public static string EscribirValidacion(Validaciones validacion, List<string> datos)
        {
            string mensaje = string.Empty;
            string lista = string.Empty;
            
            switch(validacion)
            {
                case Validaciones.Seleccion:
                    foreach (string dato in datos)
                    {
                        lista = lista + "- " + dato + "\n";
                    }
                    
                    mensaje= mensaje + "\nDebe seleccionar un elemento en las siguientes listas:\n\n" + lista;
                    break;

                case Validaciones.CompletarDatos:
                    foreach (string dato in datos)
                    {
                        lista = lista + "- " + dato + "\n";
                    }
                    mensaje = mensaje + "\nLos siguientes datos están vacíos o completados con espacios en blanco:\n\n" + lista;     
                    break;
                case Validaciones.Logica:
                    foreach (string dato in datos)
                    {
                        lista = lista + "- " + dato + "\n";
                    }
                    mensaje = mensaje + "\nPor favor revise lo siguiente:\n\n" + lista;
                    break;
              }
                        
            return mensaje;
        }

        public static string EscribirValidacion(List<ItemValidacion> datos)
        {
            string mensaje = string.Empty;
            string listaCompletar = string.Empty;
            string listaSeleccionar = string.Empty;
            string listaEspacios = string.Empty;
            string listaLogica = string.Empty;

            foreach (ItemValidacion item in datos)
            {
                switch (item.TipoValidacion)
                {
                    case Validaciones.Seleccion:
                        listaSeleccionar += "- " + item.Dato + "\n";                        
                        break;
                    case Validaciones.CompletarDatos:
                        listaCompletar+= "- " + item.Dato + "\n";
                        break;
                    case Validaciones.SoloEspacios:
                        listaEspacios += "- " + item.Dato + "\n";
                        break;
                    case Validaciones.Logica:
                        listaLogica += "- " + item.Dato + "\n";
                        break;
                }
            }
            
            if(!string.IsNullOrEmpty(listaSeleccionar)) { mensaje += "\nDebe seleccionar un elemento en las siguientes listas:\n\n" + listaSeleccionar; }
            if(!string.IsNullOrEmpty(listaCompletar)) { mensaje += "\nLos siguientes datos están vacíos o completados con espacios en blanco:\n\n" + listaCompletar; }
            if(!string.IsNullOrEmpty(listaEspacios)) { mensaje += "\nLos siguientes datos están completados con espacios en blanco:\n\n" + listaEspacios; }
            if (!string.IsNullOrEmpty(listaLogica)) { mensaje += "\nVerifique lo siguiente:\n\n" + listaLogica; }

            return mensaje;
        }


        //***************************************************************************************
        //                                  Mensajes de los ABM
        //***************************************************************************************

        //1-Mensaje sobre elemento no encontrado en la busqueda
        /// <summary>
        /// Mensaje sobre elemento no encontrado en la busqueda
        /// MSJ: No se encontraron" + elemento + " con los datos ingresados.
        /// </summary>
        /// <param name="elemento">Nombre del elemento escrito en Plural</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Text)</param>
        public static void MsjBuscarNoEncontrado(string elemento, string nombreFormulario)
        {
            MessageBox.Show("No se encontraron " + elemento + " con los datos ingresados.", nombreFormulario + " - Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //2-Mensaje de captura de excepción de error
        /// <summary>
        /// Mensaje sobre elemento no encontrado en la busqueda
        /// MSJ: Excepcion: + msjExcepcion
        /// </summary>
        /// <param name="msjExcepcion">Mensaje de la excepcion capturada (ex.Message)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Text)</param>
        /// <param name="operacion">El nombre de la operacion que se esta realizando (Obtener de la enumeracion operaciones)</param>
        public static void MsjExcepcion(string msjExcepcion, string nombreFormulario, Operaciones operacion)
        {
            MessageBox.Show("Advertencia: " + msjExcepcion, "Error: " + nombreFormulario + " - " + operacion.ToString() , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Mensaje de la Pestaña de Datos

        //3-Mensaje para confirmar la eliminacion de los datos
        /// <summary>
        /// Mensaje para que el usuario confirme la eliminacion de los datos
        /// MSJ: ¿Está seguro que desea eliminar el/la " + elemento + " seleccionado/a?
        /// </summary>
        /// <param name="elemento">Nombre del elemento escrito en singular</param>
        /// <param name="genero">Genero del elemento (Usar enumeracion Generos de la clase)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Text)</param>
        /// <returns>El elemento de la enumeracion DialogResult correspondiente </returns>  
        public static DialogResult MsjConfirmaEliminarDatos(string elemento, Generos genero, string nombreFormulario)
        {
             DialogResult respuesta= DialogResult.Yes;

            if (genero == Generos.Femenino)
            {
               respuesta = MessageBox.Show("¿Está seguro que desea eliminar la " + elemento + " seleccionada?", nombreFormulario + " - Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else if (genero == Generos.Masculino)
            {
                 respuesta = MessageBox.Show("¿Está seguro que desea eliminar el " + elemento + " seleccionado?", nombreFormulario + " - Pregunta: Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            return respuesta;
        }

        //4- Mensaje para elementos en transaccion de la pestaña de datos
        /// <summary>
        /// Mensaje para elementos en transaccion de la pestaña de datos
        /// "Excepcion: " + msjExcepcion
        /// </summary>
        /// <param name="msjExcepcion">Mensaje de la excepcion capturada (ex.Message)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Text)</param>
         
        public static void MsjElementoTransaccion(string msjExcepcion, string nombreFormulario)
        {
            MessageBox.Show("Advertencia: " + msjExcepcion, nombreFormulario + " - Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //5- Mensaje para cuando no hay seleción de elementos
        /// <summary>
        /// Mensaje para cuando no hay seleción de elementos
        /// MSJ: "Debe seleccionar un/a " + elemento + " de la lista."
        /// </summary>
        /// <param name="elemento">Nombre del elemento escrito en singular</param>
        /// <param name="genero">Genero del elemento (Usar enumeracion generos de la clase)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Text)</param>
        
        public static void MsjSinSeleccion(string elemento, Generos genero, string nombreFormulario)
        {
            if (genero ==  Generos.Femenino)
            {
                MessageBox.Show("Debe seleccionar una " + elemento + " de la lista.", nombreFormulario + " - Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (genero == Generos.Masculino)
            {
                MessageBox.Show("Debe seleccionar un " + elemento + " de la lista.", nombreFormulario + " - Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //6-Mensaje de confirmacion de guardado
        /// <summary>
        /// Mensaje de confirmacion de guardado
        /// MSJ: "Elemento" + elemento + " guardado correctamente."
        /// </summary>
        /// <param name="elemento">Nombre del elemento escrito en singular</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Text)</param>
        /// <param name="operacion">Es un elemento de la enumeracion publica Operaciones de esta clase</param>
        
        public static void MsjConfirmaGuardar(string elemento, string nombreFormulario, Operaciones operacion)
        {
            MessageBox.Show("Elemento " + elemento + " guardado correctamente.",nombreFormulario + " - Información: " + operacion.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //7-Mensaje eliminacion Correcta
        /// <summary>
        /// Mensaje que confirma que se elimino el lemento correctamente de la base de datos
        /// </summary>
        /// <param name="validacion">Elemento de las enumeraciones Validaciones de la clase Mensajes</param>
        /// <param name="operacion">Es un elemento de la enumeracion publica Operaciones de esta clase</param>
        
        public static void MsjConfirmaEliminar(string nombreFormulario, Operaciones operacion)
        {
            MessageBox.Show("El elemento seleccionado ha sido eliminado correctamente.", nombreFormulario + " - Información: " + operacion.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //8-Mensaje Multiples fallos en la validacion (Con mensajes estándares)
        /// <summary>
        /// Mensaje fallo en la validacion (Con mensajes estándares)
        /// </summary>
        /// <param name="validacion">String con el mensaje de los errores</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Text)</param>        
        public static void MsjValidacion(string validacion, string nombreFormulario)
        {
            MessageBox.Show(validacion, nombreFormulario + " - Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //9-Mensaje para efectuar una pregunta al usuario
        /// <summary>
        /// Mensaje para efectuar una pregunta al usuario, la pregunta es personalizable.
        /// </summary>
        /// <param name="pregunta">La pregunta que se desea realizar al usuario</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Text)</param>
        /// <returns>El elemento de la enumeracion DialogResult correspondiente </returns>  
        public static DialogResult MsjPreguntaAlUsuario(string pregunta, string nombreFormulario)
        {            
            return MessageBox.Show(pregunta, nombreFormulario + " - Pregunta: Seleccionar opción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);            
        }

    }
}
