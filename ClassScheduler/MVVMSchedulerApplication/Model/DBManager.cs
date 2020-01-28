using MVVMSchedulerApplication.Predmeti;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMSchedulerApplication.ViewModel;

namespace MVVMSchedulerApplication.Model
{
    public class DBManager
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string pass;

        public DBManager()
        {
            server = "localhost";
            database = "hci_2";
            uid = "hci_user";
            pass = "hci_2017";

            MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();

            connBuilder.Add("Database", database);
            connBuilder.Add("Data Source", server);
            connBuilder.Add("User Id", uid);
            connBuilder.Add("Password", pass);
            connBuilder.SslMode = MySqlSslMode.None;
            connection = new MySqlConnection(connBuilder.ConnectionString);

        }


        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                Debug.Write($"Cannot to connect to the server. Reason: {e.Message}");
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Debug.Write($"Cannot to close connection. Reason: {e.Message}");
                return false;
            }
        }

        public void ClearDatabase()
        {
            string clearAppointment = "DELETE FROM appointment";
            string clearClassroom = "DELETE FROM classroom";
            string clearClasssoft = "DELETE FROM classsoft";
            string clearCourse = "DELETE FROM course";
            string clearSoftware = "DELETE FROM software";
            string clearSubject = "DELETE FROM subject";
            string clearSubsoft = "DELETE FROM subsoft";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(clearAppointment, connection);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(clearClasssoft, connection);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(clearSubsoft, connection);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(clearClassroom, connection);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(clearSoftware, connection);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(clearSubject, connection);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(clearCourse, connection);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void ClearSingleTable(string table)
        {
            string query = "DELETE FROM @table";
            

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@table", table);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }


        #region smer

        public void InsertSmer(Smer s)
        {
            string query = "INSERT INTO course (Id, Name, IntroductionDate , Description)  VALUES (@id, @name, @date, @des)";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", s.Code);
                cmd.Parameters.AddWithValue("@name", s.Name);
                cmd.Parameters.AddWithValue("@date", s.DateOfIntroduction);
                cmd.Parameters.AddWithValue("@des", s.Description);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
        public List<Smer> SelectSmer()
        {
            string query = "SELECT * FROM course;";

            List<Smer> data = new List<Smer>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Smer m = new Smer() { Code = dataReader.GetString(0), Name = dataReader.GetString(1), DateOfIntroduction = dataReader.GetDateTime(2), Description = dataReader.GetString(3) };
                    data.Add(m);

                }

                dataReader.Close();

                this.CloseConnection();

                return data;

            }
            else
            {
                return data;
            }
        }
        public Smer FindSmerByOznaka(String oznaka)
        {
            string query = "SELECT * FROM course WHERE id = @id";
            Smer s = new Smer();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", oznaka);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    s.Code = dataReader.GetString(0);
                    s.Name = dataReader.GetString(1);
                    s.DateOfIntroduction = dataReader.GetDateTime(2);
                    s.Description = dataReader.GetString(3);
                }

                dataReader.Close();
                this.CloseConnection();
                return s;
            }
            else
            {


                return s;
            }

        }
        public void UpdateSmer(Smer s)
        {
            string query = "UPDATE course SET Name = @name, IntroductionDate = @date , Description = @des WHERE Id = @id;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", s.Code);
                cmd.Parameters.AddWithValue("@name", s.Name);
                cmd.Parameters.AddWithValue("@date", s.DateOfIntroduction);
                cmd.Parameters.AddWithValue("@des", s.Description);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
        public void DeleteSmer(Smer s)
        {
            //1. prvo pronadjemo sve predmete koji su vezni za ovaj smer
            List<Predmet> predmeti = FindPredmetBySmer(s.Code);

            //2. brisemo sve veze sa predmet-softver i sve app vezane za taj predmet
            foreach (Predmet p in predmeti)
            {
                DeleteSubSoftBySub(p.Code);
                DeleteAppBySub(p.Code);
            }
            
            //3. sad mozemo izbrisati sve predmete
            DeleteSubjectByCourse(s.Code);


            //4. brisemo trazeni smer
            string query = "DELETE FROM course WHERE Id = @id;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", s.Code);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }

        }

        #endregion smer

        #region termin
        public void DeleteAppBySub(string code)
        {
            string query = "DELETE FROM appointment WHERE Sub_id = @code";

            if(this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@code", code);

                cmd.ExecuteNonQuery();

                this.CloseConnection();

            }

        }
        public void DeleteAppByClassroom(string code)
        {
            string query = "DELETE FROM appointment WHERE Room_id = @code;";

            if(this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@code", code);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
        internal List<string> SelectTimetableName()
        {
            string query = "SELECT * FROM appointment;";
            List<string> data = new List<string>();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(reader.GetString(5));
                }


                reader.Close();
                this.CloseConnection();
                return data;


            }
            return data;
        }

        public void DeleteAppByScheduler(string name)
        {
            string query = "DELETE FROM appointment WHERE Scheduler = @name;";

            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@name", name);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        internal void InsertAppointment(MainViewModel.ClassAppointment ca, string name)
        {
            string query = "INSERT INTO appointment (Sub_id, Room_id, Start, End, Scheduler, Course_id, Description, Label, Status) VALUES (@sub, @room, @start, @end, @name, @course, @desc, @label, @status);";


            

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@sub", ca.SubjectId.ToString());
                cmd.Parameters.AddWithValue("@room", ca.ClassroomId.ToString());
                cmd.Parameters.AddWithValue("@start", ca.StartTime);
                cmd.Parameters.AddWithValue("@end", ca.EndTime);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", ca.CourseId);
                cmd.Parameters.AddWithValue("@desc", ca.Description);
                cmd.Parameters.AddWithValue("@label", ca.Label);
                cmd.Parameters.AddWithValue("@status", ca.Label);

                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        internal List<MainViewModel.ClassAppointment> SelectAppointmentByScheduler(string name)
        {
            List<MainViewModel.ClassAppointment> app = new List<MainViewModel.ClassAppointment>();

            string query = "SELECT * FROM appointment WHERE Scheduler=@name";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                MySqlDataReader reader = cmd.ExecuteReader();

                MainViewModel.ClassAppointment ca;
                while (reader.Read())
                {
                    ca = new MainViewModel.ClassAppointment()
                    {
                        SubjectId = reader.GetString(1),
                        ClassroomId = reader.GetString(2),
                        StartTime = reader.GetDateTime(3),
                        EndTime = reader.GetDateTime(4),
                        Scheduler = reader.GetString(5),
                        Description = reader.GetString(6),
                        Label = reader.GetInt16(7),
                        Status = reader.GetInt16(8),
                        CourseId = reader.GetString(9),

                    };
                    app.Add(ca);
                }

                reader.Close();

                this.CloseConnection();
                return app;
            }
            else
            {
                return app;
            }
        }

        #endregion termin


        #region predmet

        public void InsertPredmet(Predmet p)
        {
            string query = "INSERT INTO subject (Id, Name, Course_id, Description, GroupSize, LengthTerm, NumTerm, Projector, Tab ,SmartTable , Os) values (@oznaka, @naziv, @smer,@opis, @velicinaGrupe, @duzinaTermina, @brojTermina, @projektor, @tabla, @pametnaTabla, @os);";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@oznaka", p.Code);
                cmd.Parameters.AddWithValue("@naziv", p.Name);
                cmd.Parameters.AddWithValue("@smer", p.Course.Code);
                cmd.Parameters.AddWithValue("@opis", p.Description);
                cmd.Parameters.AddWithValue("@velicinaGrupe", p.GroupSize);
                cmd.Parameters.AddWithValue("@duzinaTermina", p.LengthOfTerm);
                cmd.Parameters.AddWithValue("@brojTermina", p.NumberOfTerms);
                cmd.Parameters.AddWithValue("@projektor", p.Projector);
                cmd.Parameters.AddWithValue("@tabla", p.Table);
                cmd.Parameters.AddWithValue("@pametnaTabla", p.SmartTable);
                cmd.Parameters.AddWithValue("@os", p.Os);
                cmd.ExecuteNonQuery();

                this.CloseConnection();


            }
            //uneti i sve softvere vezane za jedan predmet u tabelu subsoft
            InsertSubSoft(p);
        }

        public void UpdatePredmet(Predmet p)
        {
            string query = "UPDATE subject SET Name = @naziv, Course_id = @smer, Description = @opis, GroupSize = @velicinaGrupe, LengthTerm = @duzinaTermina, NumTerm = @brojTermina, Projector = @projektor, Tab = @tabla ,SmartTable = @pametnaTabla, Os = @os WHERE Id = @oznaka;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@oznaka", p.Code);
                cmd.Parameters.AddWithValue("@naziv", p.Name);
                cmd.Parameters.AddWithValue("@smer", p.Course.Code);
                cmd.Parameters.AddWithValue("@opis", p.Description);
                cmd.Parameters.AddWithValue("@velicinaGrupe", p.GroupSize);
                cmd.Parameters.AddWithValue("@duzinaTermina", p.LengthOfTerm);
                cmd.Parameters.AddWithValue("@brojTermina", p.NumberOfTerms);
                cmd.Parameters.AddWithValue("@projektor", p.Projector);
                cmd.Parameters.AddWithValue("@tabla", p.Table);
                cmd.Parameters.AddWithValue("@pametnaTabla", p.SmartTable);
                cmd.Parameters.AddWithValue("@os", p.Os);
                cmd.ExecuteNonQuery();

                this.CloseConnection();


            }

            //1. izbrisemo sve iz tabele subsoft vezano za ovaj predmet
            DeleteSubSoftBySub(p.Code);
            //2. unsemo nove veze izmedju softvera i predmeta
            InsertSubSoft(p);
        }

        public void DeletePredmet(PredmetDTO p)
        {
            //1. prvo moramo izbrisati vezu predmeta sa softverom
            DeleteSubSoftBySub(p.Code);

            //2. brisemo vezu predmet - termin 
            DeleteAppBySub(p.Code);

            //3. brisemo predmet iz tabele predmeta
            string query = "DELETE FROM subject WHERE Id=@id";
            if (this.OpenConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", p.Code);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }

        }

        public Predmet FindPredmetByCode(string code)
        {
            string query = "SELECT * FROM subject WHERE Id = @id;";

            Predmet p = null;

            if (this.OpenConnection() == true)
            {

                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", code);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    p = new Predmet()
                    {
                        Code = dataReader.GetString(0),
                        Name = dataReader.GetString(1),
                        Course = new Smer() { Code = dataReader.GetString(2) },
                        Description = dataReader.GetString(3),
                        GroupSize = dataReader.GetInt16(4),
                        LengthOfTerm = dataReader.GetInt16(5),
                        NumberOfTerms = dataReader.GetInt16(6),
                        Projector = dataReader.GetBoolean(7),
                        Table = dataReader.GetBoolean(8),
                        SmartTable = dataReader.GetBoolean(9),
                        Os = (Enums.OS)dataReader.GetInt16(10)
                    };
                }

                dataReader.Close();

                this.CloseConnection();

                //popunimo smerove 
                Smer s = FindSmerByOznaka(p.Course.Code);
                p.Course = s;


                //iscitati sve softvere za predmet i staviti ih u listu softvera
                List<Softver> allSoftware = new List<Softver>();
                allSoftware = SelectSubSoft(p);
                p.AllSoftware = allSoftware;

            }

            return p;

        }

        public List<Predmet> SelectPredmet()
        {
            string query = "SELECT * FROM subject;";

            List<Predmet> data = new List<Predmet>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Predmet m = new Predmet()
                    {
                        Code = dataReader.GetString(0),
                        Name = dataReader.GetString(1),
                        Course = new Smer() { Code = dataReader.GetString(2) },
                        Description = dataReader.GetString(3),
                        GroupSize = dataReader.GetInt16(4),
                        LengthOfTerm = dataReader.GetInt16(5),
                        NumberOfTerms = dataReader.GetInt16(6),
                        Projector = dataReader.GetBoolean(7),
                        Table = dataReader.GetBoolean(8),
                        SmartTable = dataReader.GetBoolean(9),
                        Os = (Enums.OS)dataReader.GetInt16(10)
                    };
                    data.Add(m);

                }

                dataReader.Close();

                this.CloseConnection();

                //popunimo smerove 
                foreach (Predmet p in data)
                {
                    Smer s = FindSmerByOznaka(p.Course.Code);
                    p.Course = s;
                }

                //iscitati sve softvere za predmet i staviti ih u listu softvera
                List<Softver> allSoftware = new List<Softver>();
                foreach (Predmet p in data)
                {
                    allSoftware = SelectSubSoft(p);
                    p.AllSoftware = allSoftware;
                }



                return data;

            }
            else
            {
                return data;
            }
        }

        public void DeleteSubjectByCourse(string code)
        {
            string query = "DELETE FROM subject WHERE Course_id = @id;";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", code);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        public PredmetDTO FindPredmetDTOById(string id)
        {

            string query = "SELECT * FROM subject WHERE Id = @id";
            PredmetDTO s = new PredmetDTO();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    s.Code = dataReader.GetString(0);
                    s.Name = dataReader.GetString(1);
                    s.Course = dataReader.GetString(2);
                    s.Description = dataReader.GetString(3);
                    s.GroupSize = dataReader.GetInt32(4);
                    s.LengthOfTerm = dataReader.GetInt32(5);
                    s.NumberOfTerms = dataReader.GetInt32(6);
                    s.Projector = dataReader.GetBoolean(7);
                    s.Table = dataReader.GetBoolean(8);
                    s.SmartTable = dataReader.GetBoolean(9);
                    s.OperationSystem = dataReader.GetString(10);

                }

                dataReader.Close();

                this.CloseConnection();

                return s;
            }
            else
            {
                return s;
            }

        }

        public List<Predmet> FindPredmetBySmer(string smer)
        {
            List<Predmet> predmeti = new List<Predmet>();

            string query = "SELECT * FROM subject WHERE Course_id = @id;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", smer);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Predmet m = new Predmet()
                    {
                        Code = dataReader.GetString(0),
                        Name = dataReader.GetString(1),
                        Course = new Smer() { Code = dataReader.GetString(2) },
                        Description = dataReader.GetString(3),
                        GroupSize = dataReader.GetInt16(4),
                        LengthOfTerm = dataReader.GetInt16(5),
                        NumberOfTerms = dataReader.GetInt16(6),
                        Projector = dataReader.GetBoolean(7),
                        Table = dataReader.GetBoolean(8),
                        SmartTable = dataReader.GetBoolean(9),
                        Os = (Enums.OS)dataReader.GetInt16(10)
                    };
                    predmeti.Add(m);

                }

                dataReader.Close();

                this.CloseConnection();

            }
            return predmeti;

        }

        #endregion predmet

        #region predmet-softver
        public void InsertSubSoft(Predmet p)
        {
            List<Softver> allSoftware = p.AllSoftware;

            string query = "INSERT INTO subsoft (Sub, Soft)  VALUES (@subject, @software)";

            if (this.OpenConnection() == true)
            {
                foreach (Softver s in allSoftware)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@subject", p.Code);
                    cmd.Parameters.AddWithValue("@software", s.Code);

                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }
        }
        
        public void DeleteSubSoftBySub(string sub)
        {
            string query = "DELETE FROM subsoft WHERE Sub = @sub;";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@sub", sub);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void DeleteSubSoftBySoft(string code)
        {
            string query = "DELETE FROM subsoft WHERE Soft = @code";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }

        }

        public List<Softver> SelectSubSoft(Predmet p)
        {
            List<Softver> data = new List<Softver>();

            string query = "SELECT * FROM subsoft where Sub = @code;";
            List<String> codeSoftware = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@code", p.Code);

                MySqlDataReader dataReader = cmd.ExecuteReader();


                while (dataReader.Read())
                {

                    codeSoftware.Add(dataReader.GetString(2));

                }

                dataReader.Close();

                this.CloseConnection();

                foreach (String s in codeSoftware)
                {
                    data.Add(FindSoftverByOznaka(s));
                }

                return data;

            }
            else
            {
                return data;
            }


        }
        #endregion predmet-softver

        #region ucionica
        public void InsertUcionica(Ucionica u)
        {
            string query = "INSERT INTO classroom (Id, Description, Projector, Tab, SmartTable, Os, NumSeats)  VALUES (@id, @des, @projector, @table, @smartTable, @os, @seats); ";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", u.Code);
                cmd.Parameters.AddWithValue("@des", u.Description);
                cmd.Parameters.AddWithValue("@projector", u.Projector);
                cmd.Parameters.AddWithValue("@table", u.Table);
                cmd.Parameters.AddWithValue("@smartTable", u.SmartTable);
                cmd.Parameters.AddWithValue("@os", u.Os);
                cmd.Parameters.AddWithValue("@seats", u.NumberOfSeats);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }

            //pozvati funkciju za uspis svih softvera raspolozivih u jednoj ucionici
            InsertClassSoftware(u);
        }

        public void UpdateUcionica(Ucionica u)
        {
            string query = "UPDATE classroom SET Description = @des, Projector = @proj, Tab = @tab, SmartTable = @st, Os = @os, NumSeats = @num WHERE  Id = @id;";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", u.Code);
                cmd.Parameters.AddWithValue("@des", u.Description);
                cmd.Parameters.AddWithValue("@proj", u.Projector);
                cmd.Parameters.AddWithValue("@tab", u.Table);
                cmd.Parameters.AddWithValue("@st", u.SmartTable);
                cmd.Parameters.AddWithValue("@os", u.Os);
                cmd.Parameters.AddWithValue("@num", u.NumberOfSeats);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }

            //prvo izbrisati sve vec postojece softvere za ovu ucionicu
            DeleteClassSoftByRoom(u.Code);
            //uneti nove softvere vezane za ucionicu
            InsertClassSoftware(u);
        }

        public void DeleteUcionica(Ucionice.UcioniceDTO udt)
        {
            //1. prvo brisemo sve veze ucionice sa softverom
            DeleteClassSoftByRoom(udt.Code);
            //2. brisemo vezu izmedju ucionice i termina 
            DeleteAppByClassroom(udt.Code);
            //2. izbrisemo ucionicu
            string query = "DELETE FROM classroom WHERE Id = @id;";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", udt.Code);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public List<Ucionica> SelectUcionica()
        {
            string query = "SELECT * FROM classroom;";

            List<Ucionica> data = new List<Ucionica>();


            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();
                Ucionica u;

                while (dataReader.Read())
                {
                    u = new Ucionica()
                    {
                        Code = dataReader.GetString(0),
                        Description = dataReader.GetString(1),
                        Projector = dataReader.GetBoolean(2),
                        Table = dataReader.GetBoolean(3),
                        SmartTable = dataReader.GetBoolean(4),
                        Os = (Enums.OS)dataReader.GetInt16(5),
                        NumberOfSeats = dataReader.GetInt16(6)
                    };
                    data.Add(u);

                }

                dataReader.Close();

                this.CloseConnection();
                //iscitati sve softvere iz tabele 
                /*
                 * 1. iz tabele classsoft povuci sve softvere za odredjenu ucionicu
                 * 2. proci kroz listu dobijenih softvera i izvuci ih tabele softvera i dodati listu u ucionicu                 * 
                 * */

                List<Softver> allSoftware = new List<Softver>();
                foreach (Ucionica uc in data)
                {
                    allSoftware = SelectClassSoft(uc);
                    uc.AllSoftware = allSoftware;
                }


                return data;

            }
            else
            {
                return data;
            }
        }

        public Ucionica FindUcionicaById(string id)
        {

            string query = "SELECT * FROM classroom WHERE Id = @id";
            Ucionica s = new Ucionica();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    s.Code = dataReader.GetString(0);
                    s.Description = dataReader.GetString(1);
                    s.Projector = dataReader.GetBoolean(2);
                    s.Table = dataReader.GetBoolean(3);
                    s.SmartTable = dataReader.GetBoolean(4);
                    s.Os = (Enums.OS)dataReader.GetInt16(5);
                    s.NumberOfSeats = dataReader.GetInt32(6);

                }

                dataReader.Close();

                this.CloseConnection();

                s.AllSoftware = SelectClassSoft(s);

                return s;
            }
            else
            {
                return s;
            }

        }

        #endregion ucionica

        #region softver-ucionica
        public void DeleteClassSoftByRoom(string id)
        {
            string query = "DELETE FROM classsoft WHERE Room = @id;";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
        public void InsertClassSoftware(Ucionica u)
        {
            List<Softver> allSoftware = u.AllSoftware;

            string query = "INSERT INTO classsoft (Room, Soft)  VALUES (@classroom, @software)";

            if (this.OpenConnection() == true)
            {
                foreach (Softver s in allSoftware)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@classroom", u.Code);
                    cmd.Parameters.AddWithValue("@software", s.Code);

                    cmd.ExecuteNonQuery();
                }
                this.CloseConnection();
            }
        }
        public List<Softver> SelectClassSoft(Ucionica u)
        {
            List<Softver> data = new List<Softver>();

            string query = "SELECT * FROM classsoft where Room = @code;";
            List<String> codeSoftware = new List<string>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@code", u.Code);
                MySqlDataReader dataReader = cmd.ExecuteReader();


                while (dataReader.Read())
                {

                    codeSoftware.Add(dataReader.GetString(2));

                }

                dataReader.Close();

                this.CloseConnection();

                foreach (String s in codeSoftware)
                {
                    data.Add(FindSoftverByOznaka(s));
                }

                return data;

            }
            else
            {
                return data;
            }


        }
        public void DeleteClassSoftBySoft(string code)
        {
            string query = "DELETE FROM classsoft WHERE Soft = @code";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }

        }

        #endregion softver-ucionica

        #region softver
        public void InsertSoftver(Softver s)
        {
            string query = "INSERT INTO software (Id, Name, Os, Manufacturer, Website, PublicationYear, Price, Description) VALUES (@oznaka, @naziv, @os, @proizvodjac, @sajt, @godina, @cena, @opis);";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@oznaka", s.Code);
                cmd.Parameters.AddWithValue("@naziv", s.Name);
                cmd.Parameters.AddWithValue("@os", s.OperationSystem);
                cmd.Parameters.AddWithValue("@proizvodjac", s.Manufacturer);
                cmd.Parameters.AddWithValue("@sajt", s.Website);
                cmd.Parameters.AddWithValue("@godina", s.YearOfPublication);
                cmd.Parameters.AddWithValue("@cena", s.Price);
                cmd.Parameters.AddWithValue("@opis", s.Description);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void UpdateSoftver(Softver s)
        {
            string query = "UPDATE software SET Name = @naziv, Os = @os, Manufacturer = @proizvodjac, Website = @sajt, PublicationYear = @godina, Price = @cena, Description = @opis WHERE Id=@oznaka;";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@oznaka", s.Code);
                cmd.Parameters.AddWithValue("@naziv", s.Name);
                cmd.Parameters.AddWithValue("@os", s.OperationSystem);
                cmd.Parameters.AddWithValue("@proizvodjac", s.Manufacturer);
                cmd.Parameters.AddWithValue("@sajt", s.Website);
                cmd.Parameters.AddWithValue("@godina", s.YearOfPublication);
                cmd.Parameters.AddWithValue("@cena", s.Price);
                cmd.Parameters.AddWithValue("@opis", s.Description);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }
        public void DeleteSoftver(Softveri.SoftverDTO softver)
        {
            //1. izbrisemo sve sto je vezano za trazeni softver u tabeli subsoft
            DeleteSubSoftBySoft(softver.Code);
            //2. izbrisemo sve sto je vezano za trazeni softver u tabeli classsoft
            DeleteClassSoftBySoft(softver.Code);

            //3.  i sad mozemo da izbrisemo softver
            string query = "DELETE FROM software WHERE Id = @code";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@code", softver.Code);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }

        }

        public List<Softver> SelectSoftver()
        {
            string query = "SELECT * FROM software;";

            List<Softver> data = new List<Softver>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Softver m = new Softver()
                    {
                        Code = dataReader.GetString(0),
                        Name = dataReader.GetString(1),
                        OperationSystem = (Enums.OS)dataReader.GetInt16(2),
                        Manufacturer = dataReader.GetString(3),
                        Website = dataReader.GetString(4),
                        YearOfPublication = dataReader.GetString(5),
                        Price = dataReader.GetDouble(6),
                        Description = dataReader.GetString(7)
                    };
                    data.Add(m);

                }

                dataReader.Close();

                this.CloseConnection();

                return data;

            }
            else
            {
                return data;
            }
        }

        public Softver FindSoftverByOznaka(string oznaka)
        {

            string query = "SELECT * FROM software WHERE Id = @oznaka";
            Softver s = new Softver();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@oznaka", oznaka);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    s.Code = dataReader.GetString(0);
                    s.Name = dataReader.GetString(1);
                    s.OperationSystem = (Enums.OS)dataReader.GetInt16(2);
                    s.Manufacturer = dataReader.GetString(3);
                    s.Website = dataReader.GetString(4);
                    s.YearOfPublication = dataReader.GetString(5);
                    s.Price = dataReader.GetDouble(6);
                    s.Description = dataReader.GetString(7);

                }

                dataReader.Close();

                this.CloseConnection();

                return s;
            }
            else
            {
                return s;
            }

        }

        public List<Softver> FindSoftwareByOs(Enums.OS os)
        {

            string query = "SELECT * FROM software WHERE os = @oznaka";
            List<Softver> data = new List<Softver>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@oznaka", (int)os);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Softver s = new Softver()
                    {
                        Code = dataReader.GetString(0),
                        Name = dataReader.GetString(1),
                        OperationSystem = (Enums.OS)dataReader.GetInt16(2),
                        Manufacturer = dataReader.GetString(3),
                        Website = dataReader.GetString(4),
                        YearOfPublication = dataReader.GetString(5),
                        Price = dataReader.GetDouble(6),
                        Description = dataReader.GetString(7)
                    };
                    data.Add(s);
                }

                dataReader.Close();

                this.CloseConnection();

                return data;
            }
            else
            {
                return data;
            }

        }
        
        #endregion softver
        
    }
}
