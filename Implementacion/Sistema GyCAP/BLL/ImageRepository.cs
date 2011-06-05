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
        private static readonly string directorioImagenes = SistemaBLL.WorkingPath + "BLL\\Img\\";

        public static void SaveImage(int codigoElemento, ElementType elementType, Image imagen)
        {
            if (imagen == null) { imagen = BLL.Properties.Resources.sinimagen; }
            string nombreImagen = string.Empty;
            
            switch (elementType)
            {
                case ElementType.Cocina:
                    if (!Directory.Exists(directorioImagenes + "Coc")) { Directory.CreateDirectory("Coc"); }
                    nombreImagen = "Coc" + codigoElemento + ".jpg";
                    break;
                case ElementType.Empleado:
                    if (!Directory.Exists(directorioImagenes + "Emp")) { Directory.CreateDirectory("Emp"); }
                    nombreImagen = "Emp" + codigoElemento + ".jpg";
                    break;
                case ElementType.Parte:
                    if (!Directory.Exists(directorioImagenes + "Part")) { Directory.CreateDirectory("Part"); }
                    nombreImagen = "Part" + codigoElemento + ".jpg";
                    break;
                default:
                    break;
            }
            
            imagen.Save(directorioImagenes + nombreImagen, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public static void GetImage(int codigoElemento, ElementType elementType)
        {

        }

        public static void DeleteImage(int codigoElemento, ElementType elementType)
        {
        }

        //private static string GetDirectory(int codigoElemento, ElementType elementType)
        //{
        //}
    }
}
