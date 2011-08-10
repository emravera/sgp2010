using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace GyCAP.BLL
{
    public class ParteBLL
    {
        public static readonly int CostoFijoChecked = 1;
        public static readonly int CostoFijoUnChecked = 0;
        
        public static void ObtenerPartes(object nombre, object codigo, object terminacion, object tipo, object estado, object plano, DataTable dtPartes)
        {
            if (Convert.ToInt32(terminacion) <= 0) { terminacion = null; }
            if (Convert.ToInt32(tipo) <= 0) { tipo = null; }
            if (Convert.ToInt32(estado) <= 0) { estado = null; }
            if (Convert.ToInt32(plano) <= 0) { plano = null; }
            DAL.ParteDAL.ObtenerPartes(nombre, codigo, terminacion, tipo, estado, plano, dtPartes);
        }

        public static int Insertar(Data.dsEstructuraProducto dsParte)
        {
            Data.dsEstructuraProducto.PARTESRow rowParte = dsParte.PARTES.GetChanges(System.Data.DataRowState.Added).Rows[0] as Data.dsEstructuraProducto.PARTESRow;
            if (DAL.ParteDAL.EsParte(rowParte.PART_NOMBRE, rowParte.PART_CODIGO, Convert.ToInt32(rowParte.PART_NUMERO))) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            return DAL.ParteDAL.Insertar(dsParte);
        }

        public static void Actualizar(Data.dsEstructuraProducto dsParte)
        {
            Data.dsEstructuraProducto.PARTESRow rowParte = dsParte.PARTES.GetChanges(System.Data.DataRowState.Modified).Rows[0] as Data.dsEstructuraProducto.PARTESRow;
            if (DAL.ParteDAL.EsParte(rowParte.PART_NOMBRE, rowParte.PART_CODIGO, Convert.ToInt32(rowParte.PART_NUMERO))) { throw new Entidades.Excepciones.ElementoExistenteException(); }
            DAL.ParteDAL.Actualizar(dsParte);
        }

        public static void Eliminar(int numeroParte)
        {
            if (!DAL.ParteDAL.PuedeEliminarse(numeroParte)) { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
            DAL.ParteDAL.Eliminar(numeroParte);
        }

        /// <summary>
        /// Guarda una imagen de una parte, si ya tiene una almacenada ésta se reemplaza.
        /// Si se llama al método sin pasar la imagen, se guarda una por defecto con la leyenda
        /// imagen no disponible.
        /// </summary>
        /// <param name="numeroParte">El número de la parte cuya imagen se quiere guardar.</param>
        /// <param name="imagen">La imagen de la parte.</param>
        public static void GuardarImagen(int numeroParte, Image imagen)
        {
            ImageRepository.SaveImage(numeroParte, ImageRepository.ElementType.Parte, imagen);
        }

        /// <summary>
        /// Obtiene la imagen de una parte, en caso de no tenerla retorna una imagen por defecto con
        /// la leyenda sin imagen.
        /// </summary>
        /// <param name="numeroParte">El número de la parte cuya imagen se quiere obtener.</param>
        /// <returns>La imagen de la parte si la tiene, caso contrario una imagen por defecto.</returns>
        public static Image ObtenerImagen(int numeroParte)
        {
            return ImageRepository.GetImage(numeroParte, ImageRepository.ElementType.Parte);
        }

        /// <summary>
        /// Elimina la imagen de una PARTE.
        /// </summary>
        /// <param name="numeroParte">El número de la parte.</param>
        public static void EliminarImagen(int numeroParte)
        {
            ImageRepository.DeleteImage(numeroParte, ImageRepository.ElementType.Parte);
        }

        /// <summary>
        /// Transforma un data row de parte a una entidad parte.
        /// </summary>
        /// <param name="row">El data row con los datos.</param>
        /// <returns>La entidad Parte.</returns>
        public static Entidades.Parte AsParteEntity(int numeroParte, Data.dsEstructuraProducto dsEstructura)
        {
            Data.dsEstructuraProducto.PARTESRow row = dsEstructura.PARTES.FindByPART_NUMERO(numeroParte);

            Entidades.Parte parte = new GyCAP.Entidades.Parte()
            {
                Numero = Convert.ToInt32(row.PART_NUMERO),
                Codigo = row.PART_CODIGO,
                Descripcion = row.PART_DESCRIPCION,
                Tipo = BLL.TipoParteBLL.AsTipoParteEntity(row.TIPOS_PARTESRow),
                Costo = row.PART_COSTO,
                Estado = BLL.EstadoParteBLL.AsEstadoParteEntity(row.ESTADO_PARTESRow),
                HojaRuta = null,
                Nombre = row.PART_NOMBRE,
                Plano = null,
                Proveedor = null,
                Terminacion = null
            };

            return parte;
        }
    }
}
