using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace GyCAP.BLL
{
    public class CocinaBLL
    {        
        public static readonly int CocinaActiva = 1;
        public static readonly int CocinaInactiva = 0;

        public static int Insertar(Entidades.Cocina cocina)
        {
            if (DAL.CocinaDAL.HayCocinaBase()) { throw new Entidades.Excepciones.CocinaBaseException("Ya existe una cocina base definida."); }
            
            return DAL.CocinaDAL.Insertar(cocina);         
        }

        public static void Actualizar(Entidades.Cocina cocina)
        {
            int codigo = 0;
            
            try
            {
                if (cocina.EsBase) { codigo = DAL.CocinaDAL.GetCodigoCocinaBase(); }
            }
            catch (Entidades.Excepciones.CocinaBaseException) { }

            if (codigo != 0 && codigo != cocina.CodigoCocina) { throw new Entidades.Excepciones.CocinaBaseException("Ya existe una cocina base definida."); }
            DAL.CocinaDAL.Actualizar(cocina);
            
        }

        public static void Eliminar(int codigoCocina)
        {
            if(!DAL.CocinaDAL.PuedeEliminarse(codigoCocina)) { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
            if (DAL.CocinaDAL.EsCocinaBase(codigoCocina)) { throw new Entidades.Excepciones.CocinaBaseException("No se puede eliminar una cocina base."); }
            DAL.CocinaDAL.Eliminar(codigoCocina); 
            //eliminar la imagen - gonzalo
        }
        
        /// <summary>
        /// Obtiene todas las cocinas sin filtrar, las carga en un DataTable del tipo cocina.
        /// </summary>
        /// <param name="dtCocina">La tabla del tipo cocina donde cargar los datos.</param>
        public static void ObtenerCocinas(DataTable dtCocina)
        {
            DAL.CocinaDAL.ObtenerCocinas(dtCocina);
        }        

        public static void ObtenerCocinas(object codigo, object codMarca, object codTerminacion, object codEstado, DataTable dtCocina)
        {
            object marca = null, terminacion = null, estado = null;
            if (codMarca != null && Convert.ToInt32(codMarca) > 0) { marca = codMarca; }
            if (codTerminacion != null && Convert.ToInt32(codTerminacion) > 0) { terminacion = codTerminacion; }
            if (codEstado != null && Convert.ToInt32(codEstado) > -1) { estado = codEstado; }
            DAL.CocinaDAL.ObtenerCocinas(codigo, marca, terminacion, estado, dtCocina);
        }

        public static bool TieneEstructuraActiva(int codigoCocina, int? codigoEstructuraOmitir)
        {
            return DAL.CocinaDAL.TieneEstructuraActiva(codigoCocina, codigoEstructuraOmitir);
        }

        /// <summary>
        /// Guarda una imagen de una cocina, si ya tiene una almacenada ésta se reemplaza.
        /// Si se llama al método sin pasar la imagen, se guarda una por defecto con la leyenda
        /// imagen no disponible.
        /// </summary>
        /// <param name="codigoCocina">El código de la cocina cuya imagen se quiere guardar.</param>
        /// <param name="imagen">La imagen de la cocina.</param>
        public static void GuardarImagen(int codigoCocina, Image imagen)
        {
            ImageRepository.SaveImage(codigoCocina, ImageRepository.ElementType.Cocina, imagen);
        }
        
        /// <summary>
        /// Obtiene la imagen de una cocina, en caso de no tenerla retorna una imagen por defecto con
        /// la leyenda sin imagen.
        /// </summary>
        /// <param name="codigoCocina">El código de la cocina cuya imagen se quiere obtener.</param>
        /// <returns>La imagen de la cocina si la tiene, caso contrario una imagen por defecto.</returns>
        public static Image ObtenerImagen(int codigoCocina)
        {
            return ImageRepository.GetImage(codigoCocina, ImageRepository.ElementType.Cocina);
        }

        /// <summary>
        /// Elimina la imagen de una cocina.
        /// </summary>
        /// <param name="codigoCocina">El código de la cocina.</param>
        public static void EliminarImagen(int codigoCocina)
        {
            ImageRepository.DeleteImage(codigoCocina, ImageRepository.ElementType.Cocina);
        }

        public static int ObtenerCodigoEstructuraActiva(int codigoCocina)
        {
            return DAL.CocinaDAL.ObtenerCodigoEstructuraActiva(codigoCocina);
        }

        public static int GetCodigoCocinaBase()
        {
            return DAL.CocinaDAL.GetCodigoCocinaBase();
        }

        public static IList<Entidades.Cocina> GetCocinasByCodigos(int[] codigosCocinas)
        {
            return DAL.CocinaDAL.GetCocinasByCodigos(codigosCocinas);
        }
        
    }
}
