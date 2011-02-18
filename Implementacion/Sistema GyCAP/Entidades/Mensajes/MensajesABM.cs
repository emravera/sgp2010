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
    public class MensajesABM
    {
        //Los siguientes son metodos que muestran mensajes estandares para los formularios de ABM


        //***************************************************************************************
        //                                  Enumeraciones Publicas
        //***************************************************************************************
        public enum Operaciones { Búsqueda, Eliminación, Guardado, Modificación }
        public enum Generos { Femenino, Masculino }
        public enum Validaciones { Seleccion }

        private static string EscribirValidacion(Validaciones validacion)
        {
            string mensaje = "";

            switch(validacion)
            {
                case Validaciones.Seleccion:
                    mensaje = "Debe seleccionar un elemento";
                    break;
            }
            return mensaje;
        }


        //***************************************************************************************
        //                                  Mensajes de los ABM
        //***************************************************************************************

        //1-Mensaje sobre elemento no encontrado en la busqueda
        /// <summary>
        /// Mensaje sobre elemento no encontrado en la busqueda
        /// </summary>
        /// <param name="elemento">Nombre del elemento escrito en Plural</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Name)</param>
        public static void MsjBuscarNoEncontrado(string elemento, string nombreFormulario)
        {
            MessageBox.Show("No se encontraron" + elemento + " con los datos ingresados.", nombreFormulario + " - Información: No hay Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //2-Mensaje de captura de excepción de error
        /// <summary>
        /// Mensaje sobre elemento no encontrado en la busqueda
        /// </summary>
        /// <param name="msjExcepcion">Mensaje de la excepcion capturada (ex.Message)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Name)</param>
        /// <param name="operacion">El nombre de la operacion que se esta realizando (Obtener de la enumeracion operaciones)</param>
        public static void MsjExcepcion(string msjExcepcion, string nombreFormulario, operaciones operacion)
        {
            MessageBox.Show(msjExcepcion, "Error:" + nombreFormulario + " - " + operacion.ToString() , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Mensaje de la Pestaña de Datos

        //3-Mensaje para confirmar la eliminacion de los datos
        /// <summary>
        /// Mensaje para que el usuario confirme la eliminacion de los datos
        /// </summary>
        /// <param name="elemento">Nombre del elemento escrito en singular</param>
        /// <param name="genero">Genero del elemento (Usar enumeracion Generos de la clase)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Name)</param>
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
        /// </summary>
        /// <param name="msjExcepcion">Mensaje de la excepcion capturada (ex.Message)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Name)</param>
         
        public static void MsjElementoTransaccion(string msjExcepcion, string nombreFormulario)
        {
            MessageBox.Show(msjExcepcion, nombreFormulario + " - Advertencia: Elemento en transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //5- Mensaje para cuando no hay seleción de elementos
        /// <summary>
        /// Mensaje para cuando no hay seleción de elementos
        /// </summary>
        /// <param name="elemento">Nombre del elemento escrito en singular</param>
        /// <param name="genero">Genero del elemento (Usar enumeracion generos de la clase)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Name)</param>
        
        public static void MsjSinSeleccion(string elemento, generos genero, string nombreFormulario)
        {
            if (genero == generos.Femenino)
            {
                MessageBox.Show("Debe seleccionar una " + elemento + " de la lista.", nombreFormulario + " - Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (genero == generos.Masculino)
            {
                MessageBox.Show("Debe seleccionar un " + elemento + " de la lista.", nombreFormulario + " - Información: Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //6-Mensaje de confirmacion de guardado
        /// <summary>
        /// Mensaje de confirmacion de guardado
        /// </summary>
        /// <param name="elemento">Nombre del elemento escrito en singular</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Name)</param>
        /// <param name="genero">Genero del elemento (Usar enumeracion genero de la clase)</param>
        
        public static void MsjConfirmaGuardar(string elemento, string nombreFormulario, operaciones operacion)
        {
            MessageBox.Show("Elemento" + elemento + " guardado correctamente.",nombreFormulario + " - Información: " + operacion.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //7-Mensaje fallo en la validacion (Con mensajes estándares)
        /// <summary>
        /// Mensaje fallo en la validacion (Con mensajes estándares)
        /// </summary>
        /// <param name="validacion">Elemento de las enumeraciones Validaciones de la clase Mensajes</param>
        /// <param name="genero">Genero del elemento (Usar enumeracion Genero de la clase)</param>
        /// <param name="nombreFormulario">El nombre del formulario (this.Name)</param>
        public static void MsjErrorValidacion(Validaciones validacion, string nombreFormulario)
        {
            MessageBox.Show(EscribirValidacion(validacion),nombreFormulario + " - Información: Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
