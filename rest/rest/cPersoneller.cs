using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace rest
{
    class cPersoneller
    {

        cGenel gnl = new cGenel();
        #region Fields
        private int personelId;
        private int gorevId;
        private string personelAd;
        private string personelSoyad;
        private string personelParola;
        private string personelKullaniciAdi;
        private bool personelDurum;

        #endregion
        #region Properties
        public int PersonelId
        {
            get => personelId;
            set => personelId = value;
        }
        public int gorevID
        {
            get => gorevId;
            set => gorevId = value;
        }
        public string PersonelAd
        {
            get => personelAd;
            set => personelAd = value;
        }
        public string PersonelSoyad
        {
            get => personelSoyad;
            set => personelSoyad = value;
        }
        public string PersonelParola
        {
            get => personelParola;
            set => personelParola = value;
        }
        public string PersonelKullaniciAdi
        {
            get => personelKullaniciAdi;
            set => personelKullaniciAdi = value;
        }
        public bool PersonelDurum { get => personelDurum; set => personelDurum = value; } 
        #endregion

        public bool personelEntryControl(string password, int UserId)
        {
            bool result = false;

            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * From Personeller where ID=@Id and PAROLA=@password",con);
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = UserId;
            cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }

            return result;
        }
        public void personelGetbyInformation(ComboBox cb)
        {
            cb.Items.Clear();

            SqlConnection con = new SqlConnection(gnl.conString);
            SqlCommand cmd = new SqlCommand("Select * From Personeller", con);

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cPersoneller p = new cPersoneller();
                p.personelId = Convert.ToInt32(dr["ID"]);
                p.gorevId = Convert.ToInt32(dr["GOREVID"]);
                p.personelAd = Convert.ToString(dr["AD"]);
                p.personelSoyad = Convert.ToString(dr["SOYAD"]);
                p.personelParola = Convert.ToString(dr["PAROLA"]);
                p.personelKullaniciAdi = Convert.ToString(dr["KULLANICIADI"]);
                p.personelDurum = Convert.ToBoolean(dr["DURUM"]);

                cb.Items.Add(p);
            }
            dr.Close();
            con.Close();
            
            
        }
        public override string ToString()
        {
            return personelAd+ " "+personelSoyad;
        }
    }
}
