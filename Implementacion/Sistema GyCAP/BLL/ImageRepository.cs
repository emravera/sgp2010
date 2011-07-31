using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace GyCAP.BLL
{
    public class ImageRepository
    {
        public enum ElementType { Cocina, Empleado, Parte };
        private static readonly string directorioImagenes = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.Replace("Principal\\bin\\Debug", ""), "BLL\\Imagenes");
        public enum ImageStatus { NoImage, New, Changed, Deleted, Same };
        public const int WithOutImage = 0;
        public const int WithImage = 1;

        public static void SaveImage(int codigoElemento, ElementType elementType, Image imagen)
        {
            if (imagen == null) { imagen = BLL.Properties.Resources.sinimagen; }
            string nombreImagen = GetFileName(codigoElemento, elementType);

            if (!Directory.Exists(directorioImagenes)) { Directory.CreateDirectory(directorioImagenes); }
            if (!Directory.Exists(Path.GetDirectoryName(nombreImagen))) { Directory.CreateDirectory(Path.GetDirectoryName(nombreImagen)); }

            try
            {
                using (FileStream file = new FileStream(nombreImagen, FileMode.Create))
                {
                    imagen.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg);
                    file.Close();
                }
            }
            catch (Exception) { throw new Entidades.Excepciones.SaveImageException(); }
        }

        public static Image GetImage(int codigoElemento, ElementType elementType)
        {
            try
            {
                string archivo = GetFileName(codigoElemento, elementType);                              
                Image img = Image.FromFile(archivo);
                Image toReturn = new Bitmap(img);
                img.Dispose();
                return toReturn;
            }
            catch (System.IO.FileNotFoundException) { return BLL.Properties.Resources.sinimagen; }
        }

        public static void DeleteImage(int codigoElemento, ElementType elementType)
        {
        }

        private static string GetFileName(int codigoElemento, ElementType elementType)
        {
            StringBuilder directorio = new StringBuilder(directorioImagenes);
            directorio.Append("\\");
            directorio.Append(elementType.ToString());
            directorio.Append("\\");
            directorio.Append(elementType.ToString());
            directorio.Append(codigoElemento);
            directorio.Append(".jpg");
            
            return directorio.ToString();            
        }
    }
}
