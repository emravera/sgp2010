using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GyCAP.BLL
{
    public class MateriaPrimaBLL
    {

        //*****************************************************************************************
        //                              BUSQUEDA MATERIAS PRIMAS
        //*****************************************************************************************

        /// <summary>
        /// Obtiene una materia prima por su código.
        /// </summary>
        /// <param name="codigoMateriaPrima">El código de la materia prima deseada.</param>
        /// <returns>El objeto materiaPrima con sus datos.</returns>
        /// <exception cref="ElementoInexistenteException">En caso de que no exista la materia prima.</exception>
        /// <exception cref="BaseDeDatosException">En caso de problemas con la base de datos.</exception>
        public static Entidades.MateriaPrima ObtenerMateriaPrima(int codigoMateriaPrima)
        {
            return DAL.MateriaPrimaDAL.ObtenerMateriaPrima(codigoMateriaPrima);
        }
        public static void ObtenerMP(System.Data.DataTable dtMateriaPrima)
        {
            DAL.MateriaPrimaDAL.ObtenerMP(dtMateriaPrima);
        }
               
        //Metodo que busca la Materia Prima desde el ABM de materias primas
        public static void ObtenerMP(string nombreMP, int esPrincipal, DataTable dtMateriaPrima)
        {
            DAL.MateriaPrimaDAL.ObtenerMP(nombreMP, esPrincipal, dtMateriaPrima);
        }

        //Metodo que devuelve el precio de una materia prima
        public static decimal ObtenerPrecioMP(decimal codigoMP)
        {
            return DAL.MateriaPrimaDAL.ObtenerPrecioMP(codigoMP);
        }

        //*****************************************************************************************
        //                              INSERTAR MATERIAS PRIMAS
        //*****************************************************************************************

        public static int Insertar(Entidades.MateriaPrima materiaPrima)
        {
            if (DAL.MateriaPrimaDAL.EsMateriaPrima(materiaPrima)) throw new Entidades.Excepciones.ElementoExistenteException();
            //Si no existe la inserta en la BAse de datos
            return DAL.MateriaPrimaDAL.Insertar(materiaPrima);
        }

        //*****************************************************************************************
        //                              ELIMINAR MATERIAS PRIMAS
        //*****************************************************************************************
        
        public static void Eliminar(int codigo)
        {
            if (DAL.MateriaPrimaDAL.ValidarEliminar(codigo)) throw new Entidades.Excepciones.ElementoEnTransaccionException();
            else DAL.MateriaPrimaDAL.Eliminar(codigo);
        }

        //*****************************************************************************************
        //                              MODIFICAR MATERIAS PRIMAS
        //*****************************************************************************************

        public static void Actualizar(Entidades.MateriaPrima materiaPrima)
        {
            if (DAL.MateriaPrimaDAL.EsMateriaPrimaActualizar(materiaPrima)) throw new Entidades.Excepciones.ElementoExistenteException();
            else if (DAL.MateriaPrimaDAL.EsMPHojaRuta(materiaPrima)) throw new Entidades.Excepciones.ElementoEnTransaccionException();
            else DAL.MateriaPrimaDAL.Actualizar(materiaPrima);
        }

        /// <summary>
        /// Transforma una data row de materia prima en una entidad de materia prima.
        /// </summary>
        /// <param name="row">El data row de materia prima.</param>
        /// <returns>La entidad Materia Prima.</returns>
        public static Entidades.MateriaPrima AsMateriaPrimaEntity(Data.dsEstructuraProducto.MATERIAS_PRIMASRow row)
        {
            Entidades.MateriaPrima mp = new GyCAP.Entidades.MateriaPrima()
            {
                CodigoMateriaPrima = Convert.ToInt32(row.MP_CODIGO),
                Cantidad = row.MP_CANTIDAD,
                CodigoUnidadMedida = Convert.ToInt32(row.UMED_CODIGO),
                Costo = row.MP_COSTO,
                Descripcion = row.MP_DESCRIPCION,
                EsPrincipal = Convert.ToInt32(row.MP_ESPRINCIPAL),
                Nombre = row.MP_NOMBRE,
                UbicacionStock =new GyCAP.Entidades.UbicacionStock(Convert.ToInt32(row.USTCK_NUMERO))
            };

            return mp;
        }

        /// <summary>
        /// Transforma una data row de materia prima en una entidad de materia prima.
        /// </summary>
        /// <param name="row">El data row de materia prima.</param>
        /// <returns>La entidad Materia Prima.</returns>
        public static Entidades.MateriaPrima AsMateriaPrimaEntity(int codigoMP, Data.dsEstructuraProducto ds)
        {
            Data.dsEstructuraProducto.MATERIAS_PRIMASRow row = ds.MATERIAS_PRIMAS.FindByMP_CODIGO(codigoMP);
            
            Entidades.MateriaPrima mp = new GyCAP.Entidades.MateriaPrima()
            {
                CodigoMateriaPrima = Convert.ToInt32(row.MP_CODIGO),
                Cantidad = row.MP_CANTIDAD,
                CodigoUnidadMedida = Convert.ToInt32(row.UMED_CODIGO),
                Costo = row.MP_COSTO,
                Descripcion = row.MP_DESCRIPCION,
                EsPrincipal = Convert.ToInt32(row.MP_ESPRINCIPAL),
                Nombre = row.MP_NOMBRE,
                UbicacionStock = (row.IsUSTCK_NUMERONull()) ? null : BLL.UbicacionStockBLL.AsUbicacionStock(Convert.ToInt32(row.USTCK_NUMERO), ds)
            };

            return mp;
        }
    }
}
