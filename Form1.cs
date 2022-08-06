using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Fitas_TandemIMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            string arquivo;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                arquivo = openFileDialog1.FileName;
            }
            else
            {
                return;
            }

            try
            {
                // lendo as linhas do arquivo
                string[] linhas = File.ReadAllLines(arquivo);

                // para cada linha dentro das linhas
                foreach (string linha in linhas)
                {
                    // separando as colunas por ; (csv)
                    string[] colunas = linha.Split(';');

                    string descricao = colunas[13];
                    int traco = descricao.LastIndexOf("-");
                    string inicio = descricao.Substring(0, traco);
                    string desc = inicio + " " + descricao.Substring(traco + 1, descricao.Length - traco - 1);

                    datagridview1.Rows.Add(desc);
                }

                // remove duplicates
                var results = datagridview1
                .Rows
                .OfType<DataGridViewRow>()
                .GroupBy(x => new { x.Cells[0].Value })
                .Select(group => new { Item = group.Key, Row = group, Count = group.Count() })
                .ToList();

                for (var index = 0; index < results.Count; index++)
                {
                    Console.WriteLine(results[index].Row.FirstOrDefault()?.Cells[0].Value);
                    results[index].Row.Skip(1)
                        .ToList()
                        .ForEach(row => datagridview1.Rows.Remove(row));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (datagridview1.Rows.Count > 0)
                {

                    Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                    xcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < datagridview1.Columns.Count + 1; i++)
                    {
                        xcelApp.Cells[1, i] = datagridview1.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < datagridview1.Rows.Count; i++)
                    {
                        for (int j = 0; j < datagridview1.Columns.Count; j++)
                        {
                            xcelApp.Cells[i + 2, j + 1] = datagridview1.Rows[i].Cells[j].Value;
                        }
                    }
                    xcelApp.Columns.AutoFit();
                    xcelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

    



