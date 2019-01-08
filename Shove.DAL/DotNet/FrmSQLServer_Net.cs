using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Shove.Database.Persistences;

namespace ShoveDAL
{
    public partial class FrmSQLServer_Net : Form
    {
        public FrmSQLServer_Net()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            MSSQL mssql = new MSSQL(tbServer.Text, tbDatabase.Text, tbUserID.Text, tbPassword.Text, tbNamespace.Text, cbConnStr.Checked,
                rbConnectionString.Checked, cbTables.Checked, cbViews.Checked, cbProcedures.Checked, cbFunctions.Checked);

            string Result = mssql.Generation();
            Clipboard.SetDataObject(Result, true);

            FrmResult fr = new FrmResult();
            fr.tbResult.Text = Result;
            fr.ShowDialog();
            fr.Dispose();
        }

        private void cbConnStr_CheckedChanged(object sender, EventArgs e)
        {
            rbConnectionString.Enabled = !cbConnStr.Checked;
            rbConnection.Enabled = rbConnectionString.Enabled;
        }

        private void btnGo_clr_Click(object sender, EventArgs e)
        {
            cbProcedures.Checked = false;
            cbFunctions.Checked = false;

            MSSQL mssql = new MSSQL(tbServer.Text, tbDatabase.Text, tbUserID.Text, tbPassword.Text, tbNamespace.Text, cbConnStr.Checked,
                rbConnectionString.Checked, cbTables.Checked, cbViews.Checked, cbProcedures.Checked, cbFunctions.Checked);

            string Result = mssql.Generation_CLR();
            Clipboard.SetDataObject(Result, true);

            FrmResult fr = new FrmResult();
            fr.tbResult.Text = Result;
            fr.ShowDialog();
            fr.Dispose();
        }
    }
}
