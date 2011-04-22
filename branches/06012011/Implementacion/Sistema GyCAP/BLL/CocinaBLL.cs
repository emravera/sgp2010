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
        /// <summary>
        /// Setea el directorio que contiene las imágenes de las cocinas en base al directorio en que
        /// se está ejecutando la aplicación.
        /// </summary>
        private static readonly string directorioImagenes = SistemaBLL.WorkingPath + "BLL\\Img\\CocImg\\";
        public static readonly int CocinaActiva = 1;
        public static readonly int CocinaInactiva = 0;

        public static int Insertar(Entidades.Cocina cocina)
        {
            return DAL.CocinaDAL.Insertar(cocina);
        }

        public static void Actualizar(Entidades.Cocina cocina)
        {
            DAL.CocinaDAL.Actualizar(cocina);
        }

        public static void Eliminar(int codigoCocina)
        {
            if(DAL.CocinaDAL.PuedeEliminarse(codigoCocina)) { DAL.CocinaDAL.Eliminar(codigoCocina); }
            else { throw new Entidades.Excepciones.ElementoEnTransaccionException(); }
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

        public static bool TieneEstructuraActiva(int codigoCocina)
        {
            return DAL.CocinaDAL.TieneEstructuraActiva(codigoCocina);
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
            if (imagen == null) { imagen = BLL.Properties.Resources.sinimagen; }
            string nombreImagen = "Coc" + codigoCocina + ".jpg";
            imagen.Save(nombreImagen, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        
        /// <summary>
        /// Obtiene la imagen de una cocina, en caso de no tenerla retorna una imagen por defecto con
        /// la leyenda sin imagen.
        /// </summary>
        /// <param name="codigoCocina">El código de la cocina cuya imagen se quiere obtener.</param>
        /// <returns>El objeto image con la imagen de la cocina si la tiene, caso contrario una imagen por defecto.</returns>
        public static Image ObtenerImagen(int codigoCocina)
        {
            try
            {
                Image imagen = Image.FromFile(directorioImagenes + "Coc" + codigoCocina + ".jpg");
                return imagen;
            }
            catch (System.IO.FileNotFoundException) { return BLL.Properties.Resources.sinimagen; }
        }

        public static int ObtenerCodigoEstructuraActiva(int codigoCocina)
        {
            return DAL.CocinaDAL.ObtenerCodigoEstructuraActiva(codigoCocina);
        }
        
    }
}
