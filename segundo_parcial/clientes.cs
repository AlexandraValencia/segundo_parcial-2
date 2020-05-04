﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace segundo_parcial
{
    public partial class clientes : Form
    {
        Conexion_db objConexion = new Conexion_db();
        int posicion = 0;
        string accion = "agregar";
        DataTable tbl = new DataTable();
        public clientes()
        {
            InitializeComponent();
        }

        private void clientes_Load(object sender, EventArgs e)
        {
            actualizarDs();
            mostrarDatos();
        }
        void actualizarDs()
        {
            tbl = objConexion.obtener_datos().Tables["clientes"];
            tbl.PrimaryKey = new DataColumn[] { tbl.Columns["idCliente"] };
        }
        void mostrarDatos()
        {
            try
            {
                lblidCliente.Text = tbl.Rows[posicion].ItemArray[0].ToString();
                txtnombre.Text = tbl.Rows[posicion].ItemArray[1].ToString();
                txtdireccion.Text = tbl.Rows[posicion].ItemArray[2].ToString();
                txttelefono.Text = tbl.Rows[posicion].ItemArray[3].ToString();
                txtdui.Text = tbl.Rows[posicion].ItemArray[4].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No hay Datos que mostrar", "Registros de Cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiar_cajas();


            }


        }
        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (btnagregar.Text == "Agregar")
            {
                btnagregar.Text = "Guardar";
                btnmodificar.Text = "Cancelar";
                accion = "agregar";

                limpiar_cajas();
                controles(false);
            }
            else
            { 
                String[] valores = {
                    lblidCliente.Text,
                    txtnombre.Text,
                    txtdireccion.Text,
                    txttelefono.Text,
                    txtdui.Text,
                  
                };
                objConexion.clientes(valores, accion);
                actualizarDs();
                posicion = tbl.Rows.Count - 1;
                mostrarDatos();

                controles(true);

                btnagregar.Text = "Agregar";
                btnmodificar.Text = "Modificar";
            }
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            if (btnmodificar.Text == "Modificar")
            {
                btnagregar.Text = "Guardar";
                btnmodificar.Text = "Cancelar";
                accion = "modificar";

                controles(false);

                btnagregar.Text = "Guardar";
                btnmodificar.Text = "Cancelar";

            }
            else
            { 
                controles(true);
                mostrarDatos();

                btnagregar.Text = "Agregar";
                btnmodificar.Text = "Modificar";
            }

        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de eliminar a " + txtnombre.Text, "Registro de Clientes",
               MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                String[] valores = { lblidCliente.Text };
                objConexion.clientes(valores, "eliminar");

                actualizarDs();
                posicion = posicion > 0 ? posicion - 1 : 0;
                mostrarDatos();
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            busqueda_clientes frmBusquedaClientes = new busqueda_clientes();
            frmBusquedaClientes.ShowDialog();

            if (frmBusquedaClientes._idCliente > 0)
            {
                posicion = tbl.Rows.IndexOf(tbl.Rows.Find(frmBusquedaClientes._idCliente));
                mostrarDatos();
            }
        }

        private void btnprimero_Click(object sender, EventArgs e)
        {
            posicion = 0;
            mostrarDatos();
        }

        private void btnanterior_Click(object sender, EventArgs e)
        {
            if (posicion > 0)
            {
                posicion--;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Primer Registro...", "Registros de Cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsiguiente_Click(object sender, EventArgs e)
        {
            if (posicion < tbl.Rows.Count - 1)
            {
                posicion++;
                mostrarDatos();
            }
            else
            {
                MessageBox.Show("Ultimo Registro...", "Registros de Cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnultimo_Click(object sender, EventArgs e)
        {
            posicion = tbl.Rows.Count - 1;
            mostrarDatos();
        }
        void limpiar_cajas()
        {
            txtnombre.Text = "";
            txtdireccion.Text = "";
            txttelefono.Text = "";
            txtdui.Text = "";

        }
        void controles(Boolean valor)
        {
           
            btneliminar.Enabled = valor;
            btnbuscar.Enabled = valor;

        }

        private void txtnombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

    


