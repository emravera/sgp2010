using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GyCAP.UI.Sistema.ControlesUsuarios
{
    
    public class AnimadorFormulario
    {
        private FormAnimator animador;
        private Form formularioParent;
        private int duracion = 0, direAnimacion = 0;
        private bool acoplado = false;
        public static readonly int animacionDerecha = 1;
        public static readonly int animacionIzquierda = 2;
        public static readonly int animacionArriba = 3;
        public static readonly int animacionAbajo = 4;

        #region Métodos públicos

        public void SetFormulario(Form formularioHijo, Form formularioPadre, int direccionAnimacion, int duracionAnimacion, bool acoplar)
        {
            if (animador == null)
            {
                animador = new FormAnimator(formularioHijo, FormAnimator.AnimationMethod.Slide, DeterminarDireccion(direccionAnimacion), duracionAnimacion);
                duracion = duracionAnimacion;
                direAnimacion = direccionAnimacion;
                acoplado = acoplar;
                formularioParent = formularioPadre;
                formularioPadre.Move += new EventHandler(formularioPadre_Move);
                formularioPadre.FormClosing += new FormClosingEventHandler(formularioPadre_FormClosing);
                formularioHijo.FormClosing += new FormClosingEventHandler(formularioHijo_FormClosing);
            }
        }        

        public void MostrarFormulario()
        {            
            if (formularioParent.Parent != null)
            {
                Point ubicacion = formularioParent.Location;
                ubicacion.X += formularioParent.Width + 1;
                animador.Form.Location = ubicacion;
                animador.Form.TopLevel = false;
                animador.Duration = 0;
                animador.Form.Show();
                animador.Duration = duracion;
                animador.Form.Parent = formularioParent.Parent;
                animador.Direction = DeterminarDireccionOpuesta(direAnimacion);
            }
            else { animador.Form.Show(); }
        }

        public void MostrarFormulario(int direccionAnimacion)
        {
            if (formularioParent.Parent != null)
            {
                Point ubicacion = formularioParent.Location;
                ubicacion.X += formularioParent.Width;
                animador.Form.Location = ubicacion;
                animador.Form.TopLevel = false;
                animador.Duration = 0;
                animador.Form.Show();
                animador.Duration = duracion;
                animador.Direction = DeterminarDireccion(direccionAnimacion);
                animador.Form.Parent = formularioParent.Parent;
            }
            else 
            {
                animador.Direction = DeterminarDireccion(direccionAnimacion);
                animador.Form.Show(); 
            }
        }

        public void CerrarFormulario()
        {
            if (animador != null)
            {
                animador.Direction = DeterminarDireccionOpuesta(direAnimacion);
                animador.Form.Dispose();
                animador = null;
            }
        }
        
        public void CerrarFormulario(int direccionAnimacion)
        {
            if (animador != null)
            {
                animador.Direction = DeterminarDireccion(direccionAnimacion);
                animador.Form.Dispose();
                animador = null;
            }
        }

        public void Acoplar()
        {
            acoplado = true;
        }

        public void Desacoplar()
        {
            acoplado = false;
        }

        public bool EsVisible()
        {
            if (animador != null) return true;
            return false;
        }

        public Form GetForm()
        {
            return animador.Form;
        }

        #endregion

        #region Métodos privados
        private FormAnimator.AnimationDirection DeterminarDireccion(int direccion)
        {
            FormAnimator.AnimationDirection dire = FormAnimator.AnimationDirection.Right;
            switch (direccion)
            {
                case 1:
                    dire = FormAnimator.AnimationDirection.Right;
                    break;
                case 2:
                    dire = FormAnimator.AnimationDirection.Left;
                    break;
                case 3:
                    dire = FormAnimator.AnimationDirection.Up;
                    break;
                case 4:
                    dire = FormAnimator.AnimationDirection.Down;
                    break;
                default:
                    break;
            }
            return dire;
        }

        private FormAnimator.AnimationDirection DeterminarDireccionOpuesta(int direccion)
        {
            FormAnimator.AnimationDirection dire = FormAnimator.AnimationDirection.Left;
            if (direccion == animacionDerecha) { dire = FormAnimator.AnimationDirection.Left; }
            if (direccion == animacionIzquierda) { dire = FormAnimator.AnimationDirection.Right; }
            if (direccion == animacionArriba) { dire = FormAnimator.AnimationDirection.Down; }
            if (direccion == animacionAbajo) { dire = FormAnimator.AnimationDirection.Up; }
            return dire;
        }
        #endregion

        #region EventHandlers

        void formularioPadre_Move(object sender, EventArgs e)
        {
            if (acoplado && animador != null)
            {
                Point ubicacion = formularioParent.Location;
                ubicacion.X += formularioParent.Width + 1;
                animador.Form.Location = ubicacion;
            }
        }

        void formularioPadre_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (animador != null)
            {
                animador.Duration = 0;
                animador.Form.Dispose();
                animador = null;
            }
        }

        void formularioHijo_FormClosing(object sender, FormClosingEventArgs e)
        {
            animador = null;
        }

        #endregion
    }
}
