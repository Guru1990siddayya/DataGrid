using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SPDataGridWP.VisualWebPart
{
    public partial class SPDataGridWPUserControl : UserControl
    {
        private SqlConnection con;
        private const string databaseServerName = "SP2013";
        private const string databaseName = "TestDB";
        private const string tableName = "gridview";
        private const string riskTable = "Risk";
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder connectionString = new StringBuilder("Data Source=");
            connectionString.Append(databaseServerName);
            connectionString.Append(";Initial Catalog=");
            connectionString.Append(databaseName);
            connectionString.Append(";Integrated Security=True;");
            con = new SqlConnection(connectionString.ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow ro = (GridViewRow)gridView.Rows[e.RowIndex];
            Label id = (Label)ro.FindControl("lblsid");
            TextBox name = (TextBox)ro.FindControl("txtName");
            TextBox address = (TextBox)ro.FindControl("txtAddress");
            DropDownList risk = (DropDownList)ro.FindControl("ddlRisk");
            UpdateGrid(Convert.ToInt32(id.Text), name.Text, address.Text, Convert.ToInt32(risk.SelectedValue));
        }

        protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridView.EditIndex = -1;
            BindGrid();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            int i = 0;
            ImageButton imgdetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)imgdetails.NamingContainer;
            Label id = (Label)gvrow.FindControl("lblsid");
            SqlCommand cmd = new SqlCommand("delete from " + tableName + " where sid=@sid", con);
            cmd.Parameters.AddWithValue("@sid", id.Text);
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
            BindGrid();
        }

        public void BindGrid()
        {
            gridView.PagerTemplate = null;
            con.Open();
            SqlCommand cmd = new SqlCommand("select mainTable.sid as sid, mainTable.name as name, mainTable.address as address, risk.RiskName as RiskName, risk.RiskId as RiskId from " + tableName + " as mainTable inner join " + riskTable + " as risk on mainTable.RiskId = risk.RiskId", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gridView.DataSource = ds;
            gridView.DataBind();
            con.Close();
        }

        private void UpdateGrid(int sid, string name, string address, int RiskId)
        {
            try
            {
                string query = "UPDATE " + tableName + " SET address = @address, name = @name, RiskId=@riskid  WHERE sid = @sid";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@sid", SqlDbType.Int).Value = sid;
                com.Parameters.Add("@address", SqlDbType.VarChar).Value = address;
                com.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                com.Parameters.Add("@riskid", SqlDbType.Int).Value = RiskId;

                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                gridView.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1 && gridView.EditIndex == e.Row.RowIndex)
            {
                SqlDataReader ddr = null;

                SqlCommand cmd = new SqlCommand("select * from Risk", con);
                ddr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DropDownList ddl;

                ddl = (DropDownList)e.Row.Cells[3].FindControl("ddlRisk");
                ddl.DataSource = ddr;
                ddl.DataTextField = "RiskName";
                ddl.DataValueField = "RiskId";
                ddl.DataBind();
            }
        }
    }
    
}
