using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empleados
{
    public partial class frmCaso : Form
    {
        ManejoDatos mdt=new ManejoDatos();
        public frmCaso()
        {

            InitializeComponent();
        }

        private void bloquearCeldas(bool estado)
        {
            txtReso.Enabled= estado;
            txtMotivo.Enabled= estado;
            txtSusp.Enabled = estado;
            txtSector.Enabled= estado;
            txtPuesto.Enabled= estado;
        }

       private void button1_Click(object sender, EventArgs e)
        {
            var empleado = mdt.buscarEmpleado(txtBusqueda.Text);
            if (empleado != null)
            {
                txtLeg.Text = empleado.Legajo;
                txtNomb.Text = empleado.Nombre;
                txtApe.Text = empleado.Apellido;
                txtSector.Text = empleado.Sector;
                txtPuesto.Text = empleado.Puesto;
                bloquearCeldas(false);
            }
            else
            {
                MessageBox.Show("Empleado no encontrado.", "Modulo de empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        int contcaso = 12;
        private void btnImpresion_Click(object sender, EventArgs e)
        {
                
            impresion imp=new impresion();
            imp.sector= txtSector.Text;
            imp.puesto= txtPuesto.Text;
            imp.Nombre = txtNomb.Text;
            imp.Apellido= txtApe.Text;
            imp.Legajo=txtLeg.Text;
            imp.motivo = motivo;
            imp.resolucion = resolucion;
            imp.codmotivo = cbSancion.SelectedItem.ToString();
            imp.codresolucion=cbResolucion.SelectedItem.ToString();
            imp.nroCaso=contcaso.ToString();
            imp.Fecha = dtFecha.Value.ToString("yyyy/MM/dd");
            imp.cantSusp=txtSusp.Text;
            imp.ShowDialog();

        }

        string motivo;
        int indice = 0;
        private readonly string[] motivos = {
            "Ausencia sin justificación", 
            "Ausencia sin aviso ni justificación", 
            "Impuntualidad", 
            "Abandono de tareas", 
            "Actos de indisciplina", 
            "Indisciplina con superiores",
            "Incumplimiento de tareas", 
            "Negativa a realizar tareas", 
            "Incumplimiento de normas", 
            "Provocar riesgos de seguridad", 
            "Mala fe laboral", 
            "Falta de colab. y/o fidelidad", 
            "Robo o hurto de materiales", 
            "Negligencia en las tareas", 
            "Emite caso", 
            "Fallas en la producción"
        };

        private void cbSancion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = cbSancion.SelectedIndex;
            if(indice >= 0 && indice < motivos.Length)
            {
                txtMotivo.Text = motivos[indice];
            }
            else
            {
                txtMotivo.Text = "";
            }
        }


        string resolucion;
            int posicion = 0;
        
        private readonly string[] resoluciones = {
            "Llamado de atención", 
            "Apercibimiento", 
            "Suspensión", 
            "Suspensión condicionada", 
            "Intimación retomar tareas",
            "Despido con causa", 
            "Despido sin causa", 
            "Ext. Mutuo acuerdo", 
            "Se acepta descargo", 
            "Se anula Caso", 
            "Menciones especiales", 
            "Observaciones varias", 
            "Emite caso"
        };
        private void cbResolucion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int posicion = cbResolucion.SelectedIndex;
            if(posicion >= 0 && posicion < resoluciones.Length)
            {
                txtReso.Text = resoluciones[posicion];
                if (posicion == 2 || posicion == 3)
                {
                    txtSusp.Enabled = true;
                }
            }
            else
            {
                txtReso.Text = "";
            }
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {
            bloquearCeldas(false);

        }
    }
}
