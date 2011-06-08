using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace GyCAP.BLL
{
    public class EmpleadoBLL : UsuarioBLL
    {
        public static readonly int BuscarPorLegajo = 1;
        public static readonly int BuscarPorApellido = 2;
        public static readonly int BuscarPorNombre = 3;

        //Busqueda
        //Obtiene los datos de acuerdo a los criterios de busqueda
        public static void ObtenerTodos(object buscarPor, object nombre, object codEstado, object codSector, Data.dsEmpleado ds)
        {
            object estado = null, sector = null;
            if (codEstado != null && Convert.ToInt32(codEstado) > 0) { estado = codEstado; }
            if (codSector != null && Convert.ToInt32(codSector) > 0) { sector = codSector; }
            DAL.EmpleadoDAL.ObtenerEmpleado(buscarPor, nombre, estado, sector, ds);
        }

        //Eliminacion
        public static void Eliminar(long codigo)
        {
            //Revisamos que no esté en alguna transacción
            if (DAL.EmpleadoDAL.PuedeEliminarse(codigo))
            {
                //Puede eliminarse
                DAL.EmpleadoDAL.Eliminar(codigo);
            }
            else
            {
                //No puede eliminarse, lanzamos nuestra excepción
                throw new Entidades.Excepciones.ElementoEnTransaccionException();
            }
        }

        //Guardado de Datos
        public static int Insertar(Data.dsEmpleado dsEmpleado)
        {
            //Si existe lanzamos la excepción correspondiente
            Entidades.Empleado empleado = new GyCAP.Entidades.Empleado();
            Data.dsEmpleado.EMPLEADOSRow row = dsEmpleado.EMPLEADOS.GetChanges(DataRowState.Added).Rows[0] as Data.dsEmpleado.EMPLEADOSRow;
            empleado.Codigo = Convert.ToInt32(row.E_CODIGO);
            empleado.Legajo = row.E_LEGAJO;
            if (EsEmpleado(empleado)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Como no existe lo creamos
            return DAL.EmpleadoDAL.Insertar(dsEmpleado);
        }

        //Metodo que valida que no se este guardando algo que ya existe
        public static bool EsEmpleado(Entidades.Empleado empleado)
        {
            return DAL.EmpleadoDAL.esEmpleado(empleado);
        }
        //Actualización de los datos
        public static void Actualizar(Data.dsEmpleado dsEmpleado)
        {
            //Si existe lanzamos la excepción correspondiente
            Entidades.Empleado empleado = new GyCAP.Entidades.Empleado();
            Data.dsEmpleado.EMPLEADOSRow row = dsEmpleado.EMPLEADOS.GetChanges(DataRowState.Modified).Rows[0] as Data.dsEmpleado.EMPLEADOSRow;
            empleado.Codigo = Convert.ToInt32(row.E_CODIGO);
            empleado.Legajo = row.E_LEGAJO;
            if (EsEmpleado(empleado)) throw new Entidades.Excepciones.ElementoExistenteException();
            DAL.EmpleadoDAL.Actualizar(dsEmpleado);
        }

        /// <summary>
        /// Obtiene todos los empleados sin filtrar, los carga en una DataTable del tipo empleados.
        /// </summary>
        /// <param name="dtEmpleados">La tabla donde cargar los datos.</param>
        public static void ObtenerEmpleados(DataTable dtEmpleados)
        {
            DAL.EmpleadoDAL.ObtenerEmpleados(dtEmpleados);
        }

        public static void ObtenerEmpleados(Data.dsMantenimiento dsMantenimeinto)
        {
            DAL.EmpleadoDAL.ObtenerEmpleados(dsMantenimeinto);
        }

        /// <summary>
        /// Guarda un foto de un empleado, si ya tiene una almacenada ésta se reemplaza.
        /// Si se llama al método sin pasar la imagen, se guarda una por defecto con la leyenda
        /// imagen no disponible.
        /// </summary>
        /// <param name="codigoCocina">El código del empleado cuya foto se quiere guardar.</param>
        /// <param name="imagen">La foto del empleado.</param>
        public static void GuardarFoto(int codigoEmpleado, Image imagen)
        {
            ImageRepository.SaveImage(codigoEmpleado, ImageRepository.ElementType.Empleado, imagen);
        }

        /// <summary>
        /// Obtiene la foto de un empleado, en caso de no tenerla retorna una imagen por defecto con
        /// la leyenda sin imagen.
        /// </summary>
        /// <param name="codigoEmpleado">El código del empleado cuya foto se quiere obtener.</param>
        /// <returns>La foto del empleado si la tiene, caso contrario una imagen por defecto.</returns>
        public static Image ObtenerFoto(int codigoEmpleado)
        {
            return ImageRepository.GetImage(codigoEmpleado, ImageRepository.ElementType.Empleado);
        }

        /// <summary>
        /// Elimina la foto de un empleado.
        /// </summary>
        /// <param name="codigoEmpleado">El código del empleado.</param>
        public static void EliminarFoto(int codigoEmpleado)
        {
            ImageRepository.DeleteImage(codigoEmpleado, ImageRepository.ElementType.Empleado);
        }
    }
}
