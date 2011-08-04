using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyCAP.UI.Sistema.Validaciones
{
    public class NumericLimitValues
    {
        public enum IncludeExclude { Inclusivo, Exclusivo };

        public NumericLimitValues(string valorMinimo, string valorMaximo)
        {
            this.minValue = Convert.ToDecimal(valorMinimo);
            this.maxValue = Convert.ToDecimal(valorMaximo);
            SetErrorMessage(string.Empty);
            this.margenInferior = IncludeExclude.Inclusivo;
            this.margenSuperior = IncludeExclude.Inclusivo;
        }
        
        public NumericLimitValues(string valorMinimo, string valorMaximo, string mensaje)
        {
            this.minValue = Convert.ToDecimal(valorMinimo);
            this.maxValue = Convert.ToDecimal(valorMaximo);
            SetErrorMessage(mensaje);
            this.margenInferior = IncludeExclude.Inclusivo;
            this.margenSuperior = IncludeExclude.Inclusivo;
        }

        public NumericLimitValues(string valorMinimo, IncludeExclude margenMinimo, string valorMaximo, IncludeExclude margenMaximo)
        {
            this.minValue = Convert.ToDecimal(valorMinimo);
            this.maxValue = Convert.ToDecimal(valorMaximo);
            SetErrorMessage(string.Empty);
            this.margenInferior = margenMinimo;
            this.margenSuperior = margenMaximo;
        }

        public NumericLimitValues(string valorMinimo, IncludeExclude margenMinimo, string valorMaximo, IncludeExclude margenMaximo, string mensaje)
        {
            this.minValue = Convert.ToDecimal(valorMinimo);
            this.maxValue = Convert.ToDecimal(valorMaximo);
            SetErrorMessage(mensaje);
            this.margenInferior = margenMinimo;
            this.margenSuperior = margenMaximo;
        }
        
        decimal minValue;

        public decimal MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }
        decimal maxValue;

        public decimal MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        string onErrorMessage;

        public string OnErrorMessage
        {
            get { return onErrorMessage; }
            set { onErrorMessage = value; }
        }

        private IncludeExclude margenInferior;

        public IncludeExclude MargenInferior
        {
            get { return margenInferior; }
            set { margenInferior = value; }
        }
        private IncludeExclude margenSuperior;

        public IncludeExclude MargenSuperior
        {
            get { return margenSuperior; }
            set { margenSuperior = value; }
        }

        private void SetErrorMessage(string mensaje)
        {
            if (mensaje == string.Empty)
            {
                this.onErrorMessage = string.Concat("El rango del valor es: ",
                    string.Concat(MinValue, (this.margenInferior == IncludeExclude.Inclusivo) ? " inclusivo" : " exclusivo"),
                    " a ",
                    string.Concat(MaxValue, (this.margenSuperior == IncludeExclude.Exclusivo) ? " inclusivo" : " exclusivo."));
            }
            else
            {
                this.onErrorMessage = mensaje;
            }
        }
        
        public bool IsValid(System.Windows.Forms.NumericUpDown nud)
        {
            bool valid = true;

            if (this.margenInferior == IncludeExclude.Inclusivo)
            {
                if (nud.Value < this.minValue) { valid = false; }
            }
            else
            {
                if (nud.Value <= this.minValue) { valid = false; }
            }

            if (this.margenSuperior == IncludeExclude.Inclusivo)
            {
                if (nud.Value > this.maxValue) { valid = false; }
            }
            else
            {
                if (nud.Value >= this.maxValue) { valid = false; }
            }

            return valid;
        }
    }
}
