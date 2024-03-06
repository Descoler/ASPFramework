using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ASPFramework.Pages
{
    public partial class ProductoPaginado : System.Web.UI.Page
    {
        ValuesController values = new ValuesController();
        ValuesController.Respuesta respuesta;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Label2.Text == "0")
            {
                respuesta = values.GetProductosPaginados("");
                escribeDatos(respuesta);
                Label2.Text = respuesta.Pagination.Page;

                ActURL.Text = respuesta.Pagination.Links.Actual;
                
                if (respuesta.Pagination.Links.Previous == null)
                {
                    PrevURL.Text = "";
                    Prev.Enabled = false;
                } else
                {
                    PrevURL.Text = respuesta.Pagination.Links.Previous;
                    Prev.Enabled = true;
                }
                if (respuesta.Pagination.Links.Next == null)
                {
                    NextURL.Text = "";
                    Next.Enabled = false;
                } else
                {
                    NextURL.Text = respuesta.Pagination.Links.Next;
                    Next.Enabled = true;
                }
                
            }
        }

        protected void Prev_Click(object sender, EventArgs e)
        {
            if (PrevURL.Text != "")
            {
                respuesta = values.GetProductosPaginados(PrevURL.Text, checkOrden(recogeParms()));
                escribeDatos(respuesta);
                Label2.Text = respuesta.Pagination.Page;
                ActURL.Text = respuesta.Pagination.Links.Actual;
            } 
            if (respuesta.Pagination.Links.Previous == null)
            {
                PrevURL.Text = "";
                Prev.Enabled = false;
            }
            else
            {
                PrevURL.Text = respuesta.Pagination.Links.Previous;
                Prev.Enabled = true;
            }
            if (respuesta.Pagination.Links.Next == null)
            {
                NextURL.Text = "";
                Next.Enabled = false;
            } else
            {
                NextURL.Text = respuesta.Pagination.Links.Next;
                Next.Enabled = true;
            }
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            if (NextURL.Text != "")
            {
                respuesta = values.GetProductosPaginados(NextURL.Text, checkOrden(recogeParms()));
                escribeDatos(respuesta);
                Label2.Text = respuesta.Pagination.Page;
                ActURL.Text = respuesta.Pagination.Links.Actual;
            }
            if (respuesta.Pagination.Links.Next == null)
            {
                NextURL.Text = "";
                Next.Enabled = false;
            } else
            {
                NextURL.Text = respuesta.Pagination.Links.Next;
                Next.Enabled= true;
            }
            if (respuesta.Pagination.Links.Previous == null)
            {
                Prev.Enabled = false;
                PrevURL.Text = "";
            } else
            {
                PrevURL.Text = respuesta.Pagination.Links.Previous;
                Prev.Enabled = true;
            }
        }

        protected void escribeDatos (ValuesController.Respuesta respuesta)
        {
            int numcells = 4;
            int numrows = respuesta.Data.Count;
            foreach (ValuesController.Producto prod in  respuesta.Data)
            {
                TableRow r = new TableRow();
                for (int i = 0; i < numcells; i++)
                {
                    TableCell c = new TableCell();
                    string fotourl = values.GetFoto(prod.CodigoProducto);
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    if (!File.Exists(path.Remove(0, 6) + "\\" + fotourl))
                    {
                        fotourl = @"..\Resources\Images\SinFoto.jpg";
                    }
                    string codprod = prod.CodigoProducto;
                    string titulo = prod.Titulo;
                    string precio = prod.Precio;
                    if (i == 0)
                    {
                        c.Controls.Add(new LiteralControl("<img id=\"Img1\" src=\"" + fotourl + "\" height=\"100\" width=\"100\" />"));
                        r.Cells.Add(c);
                    }
                    if (i == 1)
                    {
                        c.Controls.Add(new LiteralControl(codprod));
                        r.Cells.Add(c);
                    }
                    if (i == 2)
                    {
                        c.Controls.Add(new LiteralControl(titulo));
                        r.Cells.Add(c);
                    }
                    if (i == 3)
                    {
                        c.Controls.Add(new LiteralControl(precio));
                        r.Cells.Add(c);

                    }
                }
                Table1.Rows.Add(r);
            }

            Table1.Visible = true;
        }

        protected string checkOrden(string parametros)
        {
            string respuesta = "";
            if (RdbCodAsc.Checked == true) { respuesta = "orden=codigoasc"; }
            if (RdbCodDsc.Checked == true) { respuesta = "orden=codigodesc"; }
            if (RdbTitAsc.Checked == true) { respuesta = "orden=tituloasc"; }
            if (RdbTitDsc.Checked == true) { respuesta = "orden=titulodesc"; }

            if (parametros != "" & respuesta != "") { return parametros + "&" + respuesta; }
            else if (parametros != "" & respuesta == "") { return parametros; }
            return respuesta;
            
        }

        protected string recogeParms()
        {
            string parametros = "";
            if (TxtBuscar.Text != "")
            {
                if (RdbBuscaTit.Checked == true)
                {
                    parametros = "buscar=" + TxtBuscar.Text;
                }
                if (RdbBuscaCod.Checked == true)
                {
                    parametros = "codigobarras=" + TxtBuscar.Text;
                }
            }

            return parametros;
        }

        protected void BtnBusca_Click(object sender, EventArgs e)
        {
            if (TxtBuscar.Text == "")
            {
                respuesta = values.GetProductosPaginados(checkOrden(""));
                escribeDatos(respuesta);
                Label2.Text = respuesta.Pagination.Page;

                ActURL.Text = respuesta.Pagination.Links.Actual;

                if (respuesta.Pagination.Links.Previous == null)
                {
                    PrevURL.Text = "";
                    Prev.Enabled = false;
                }
                else
                {
                    PrevURL.Text = respuesta.Pagination.Links.Previous;
                    Prev.Enabled = true;
                }
                if (respuesta.Pagination.Links.Next == null)
                {
                    NextURL.Text = "";
                    Next.Enabled = false;
                }
                else
                {
                    NextURL.Text = respuesta.Pagination.Links.Next;
                    Next.Enabled = true;
                }
            }
            else
            {
                string busca = recogeParms();
                respuesta = values.GetProductosPaginados(checkOrden(busca));
                escribeDatos(respuesta);
                Label2.Text = respuesta.Pagination.Page;
                ActURL.Text = respuesta.Pagination.Links.Actual + "&" + busca;
                if (respuesta.Pagination.Links.Previous == null)
                {
                    PrevURL.Text = "";
                    Prev.Enabled = false;
                }
                else
                {
                    PrevURL.Text = respuesta.Pagination.Links.Previous;
                    Prev.Enabled = true;
                }
                if (respuesta.Pagination.Links.Next == null)
                {
                    NextURL.Text = "";
                    Next.Enabled = false;
                }
                else
                {
                    NextURL.Text = respuesta.Pagination.Links.Next;
                    Next.Enabled = true;
                }
                TxtBuscar.Text = "";
            }

        }

        protected void Rdb_CheckedChanged(object sender, EventArgs e)
        {
            respuesta = values.GetProductosPaginados(ActURL.Text,checkOrden(""));
            escribeDatos(respuesta);
            Label2.Text = respuesta.Pagination.Page;

            if (respuesta.Pagination.Links.Previous == null)
            {
                PrevURL.Text = "";
                Prev.Enabled = false;
            }
            else
            {
                PrevURL.Text = respuesta.Pagination.Links.Previous;
                Prev.Enabled = true;
            }
            if (respuesta.Pagination.Links.Next == null)
            {
                NextURL.Text = "";
                Next.Enabled = false;
            }
            else
            {
                NextURL.Text = respuesta.Pagination.Links.Next;
                Next.Enabled = true;
            }
        }
    }
}