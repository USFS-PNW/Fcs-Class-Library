using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
//using ODP;
using System.Windows.Forms;
using Oracle.ADO;


namespace FcsClassLibrary
{

    #region FCSEntities
    public partial class FCSEntities
    {
        #region Constructors

        public static int m_intError = 0;
        public static string m_strError = "";

        

        private bool _bDisplayErrors = true;
        public bool DisplayErrors
        {
            get { return _bDisplayErrors; }
            set { _bDisplayErrors = value; }
        }
        

        private List<BIOSUM_VOLUME> _BIOSUM_VOLUME_LIST;
        public List<BIOSUM_VOLUME> BIOSUM_VOLUME_LIST
        {
            get { return _BIOSUM_VOLUME_LIST; }
            set { _BIOSUM_VOLUME_LIST = value; }
        		
        }
        private FCSOracle _OracleADO;
        public FCSOracle OracleADO
        {
            get { return _OracleADO; }
            set { _OracleADO = value; }
        }

        public FCSEntities()
        {
            _OracleADO = new FCSOracle();
        }
        public FCSEntities(FCSOracle p_FCSOracle)
        {
            OracleADO = p_FCSOracle;
        }

        #endregion
        #region Methods

        public void InitializeADOOracleObject()
        {
            if (_OracleADO != null)
            {
                if (_OracleADO.m_Connection != null && _OracleADO.m_Connection.State != System.Data.ConnectionState.Closed)
                {
                    if (_OracleADO.m_DataReader != null) _OracleADO.m_DataReader.Dispose();

                    if (_OracleADO.m_Command != null) _OracleADO.m_Command.Dispose();

                    if (_OracleADO.m_DataAdapter != null)
                    {
                        if (_OracleADO.m_DataAdapter.SelectCommand != null)
                        {
                            _OracleADO.m_DataAdapter.SelectCommand.Dispose();
                        }
                        if (_OracleADO.m_DataAdapter.UpdateCommand != null)
                        {
                            _OracleADO.m_DataAdapter.UpdateCommand.Dispose();
                        }
                        if (_OracleADO.m_DataAdapter.DeleteCommand != null)
                        {
                            _OracleADO.m_DataAdapter.DeleteCommand.Dispose();
                        }
                        if (_OracleADO.m_DataAdapter.InsertCommand != null)
                        {
                            _OracleADO.m_DataAdapter.InsertCommand.Dispose();
                        }
                    }
                    _OracleADO.CloseConnection(_OracleADO.m_Connection);
                }



                if (_OracleADO.m_Connection != null) _OracleADO.m_Connection.Dispose();

                _OracleADO = null;

                System.Threading.Thread.Sleep(5000); //sleep 5 seconds


            }
            _OracleADO = new FCSOracle();

        }

        public void OracleConnect()
        {
            try
            {
                if (_OracleADO.m_Connection != null && _OracleADO.m_Connection.State != System.Data.ConnectionState.Closed)
                {
                    if (_OracleADO.m_DataReader != null) _OracleADO.m_DataReader.Dispose();

                    if (_OracleADO.m_Command != null) _OracleADO.m_Command.Dispose();

                    if (_OracleADO.m_DataAdapter != null)
                    {
                        if (_OracleADO.m_DataAdapter.SelectCommand != null)
                        {
                            _OracleADO.m_DataAdapter.SelectCommand.Dispose();
                        }
                        if (_OracleADO.m_DataAdapter.UpdateCommand != null)
                        {
                            _OracleADO.m_DataAdapter.UpdateCommand.Dispose();
                        }
                        if (_OracleADO.m_DataAdapter.DeleteCommand != null)
                        {
                            _OracleADO.m_DataAdapter.DeleteCommand.Dispose();
                        }
                        if (_OracleADO.m_DataAdapter.InsertCommand != null)
                        {
                            _OracleADO.m_DataAdapter.InsertCommand.Dispose();
                        }
                    }
                    _OracleADO.CloseConnection(_OracleADO.m_Connection);
                }



                System.Threading.Thread.Sleep(2000); //sleep 2 seconds

                if (_OracleADO.m_Connection != null) _OracleADO.m_Connection.Dispose();

                _OracleADO.m_strConnection = FCSOracle.FCSConnectionString;

                _OracleADO.OpenConnection(_OracleADO.m_strConnection);

            }

            //catch (Oracle.DataAccess.Client.OracleException errOracle)
            //{
            //    m_strError = errOracle.Message;
            //    m_intError = -1;
            //}
            catch (System.Data.OracleClient.OracleException errOracle)
            {
                m_strError = errOracle.Message;
                m_intError = -1;
            }
            catch (Exception err)
            {
                m_strError = err.Message;
                m_intError = -1;
            }
            finally
            {
                if (m_intError != 0 || _OracleADO.m_intError != 0)
                {
                    _OracleADO.CloseConnection(_OracleADO.m_Connection);
                }
                else
                {

                    if (_OracleADO.m_DataSet != null)
                    {
                        _OracleADO.m_DataSet.Tables.Clear();
                        _OracleADO.m_DataSet.Dispose();
                    }

                    _OracleADO.m_DataSet = new System.Data.DataSet("FCS");
                }

            }

        }


        /// <summary>
        /// There are no comments for COMPUTE_BIOSUM_VOLUMES_PKG in the schema.
        /// </summary>
        public virtual void COMPUTE_BIOSUM_VOLUMES_PKG ()
        {
            m_intError = 0; m_strError = "";
            bool bCloseConnection = false;
            if (_OracleADO.ConnectionDisposed || 
                _OracleADO.m_Connection == null || 
                (_OracleADO.m_Connection != null && 
                 _OracleADO.m_Connection.State != ConnectionState.Open))
            {
                OracleConnect();
                bCloseConnection = true;
            }
            if (_OracleADO.m_intError == 0)
            {

                try
                {
                    //using (Oracle.DataAccess.Client.OracleCommand command = new Oracle.DataAccess.Client.OracleCommand())
                    using (System.Data.OracleClient.OracleCommand command = new System.Data.OracleClient.OracleCommand())
                    {
                        command.Connection = _OracleADO.m_Connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = FCSOracle.FCSSchema + ".COMPUTE_BIOSUM_VOLUMES_PKG";
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    m_intError = -1;
                    m_strError = e.Message;
                    if (DisplayErrors)
                        MessageBox.Show("!!Error!! \n" +
                            "Module - FCSEntities:REMOVE_APP_ROLE_FROM_USER  \n" +
                            "Err Msg - " + e.Message.ToString().Trim(),
                            "BIOSUM", System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Exclamation);

                }
                finally
                {
                    if (bCloseConnection)
                    {
                        //close connection and release resources
                        OracleADO.CloseAndDisposeConnection(_OracleADO.m_Connection, false);
                        
                    }
                }
            }
        }

    
        /// <summary>
        /// There are no comments for COMP_BIOSUM_VOLS_BY_CURSOR in the schema.
        /// </summary>
        public virtual void COMP_BIOSUM_VOLS_BY_CURSOR ()
        {
           
            m_intError = 0; m_strError = "";
            bool bCloseConnection = false;
            if (_OracleADO.ConnectionDisposed || 
                _OracleADO.m_Connection == null || 
                (_OracleADO.m_Connection != null && 
                 _OracleADO.m_Connection.State != ConnectionState.Open))
            {
                OracleConnect();
                bCloseConnection = true;
            }
            if (_OracleADO.m_intError == 0)
            {
                try
                {
                    //using (Oracle.DataAccess.Client.OracleCommand command = new Oracle.DataAccess.Client.OracleCommand())
                    using (System.Data.OracleClient.OracleCommand command = new System.Data.OracleClient.OracleCommand())
                    {
                        command.Connection = _OracleADO.m_Connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = FCSOracle.FCSSchema + ".COMPUTE_BIOSUM_VOLUMES_PKG.COMP_BIOSUM_VOLS_BY_CURSOR";
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    m_intError = -1;
                    m_strError = e.Message;
                    if (DisplayErrors)
                        MessageBox.Show("!!Error!! \n" +
                            "Module - FCSEntities:COMP_BIOSUM_VOLS_BY_CURSOR  \n" +
                            "Err Msg - " + e.Message.ToString().Trim(),
                            "BIOSUM", System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Exclamation);

                }
                finally
                {
                    if (bCloseConnection)
                    {
                        //close connection and release resources
                        OracleADO.CloseAndDisposeConnection(_OracleADO.m_Connection, false);
                        
                    }
                }
                
            }
        }

        public void DeleteAllRecords_BIOSUM_VOLUME()
        {
            m_intError = 0; m_strError = "";
            bool bCloseConnection = false;
            if (_OracleADO.ConnectionDisposed || 
                _OracleADO.m_Connection == null || 
                (_OracleADO.m_Connection != null && 
                 _OracleADO.m_Connection.State != ConnectionState.Open))
            {
                OracleConnect();
                bCloseConnection = true;
            }
            if (_OracleADO.m_intError == 0)
            {
                try
                {
                    _OracleADO.SqlNonQuery(_OracleADO.m_Connection,"DELETE FROM " + FCSOracle.FCSSchema + ".BIOSUM_VOLUME");

                }
                catch (Exception e)
                {
                    m_intError = -1;
                    m_strError = e.Message;
                    if (DisplayErrors)
                        MessageBox.Show("!!Error!! \n" +
                            "Module - FCSEntities:Delete_BIOSUM_VOLUME  \n" +
                            "Err Msg - " + e.Message.ToString().Trim(),
                            "BIOSUM", System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Exclamation);

                }
                finally
                {
                    if (bCloseConnection)
                    {
                        //close connection and release resources
                        OracleADO.CloseAndDisposeConnection(_OracleADO.m_Connection, false);

                    }
                }
            }
        }

        public void SQLNonQuery(string p_strSQL)
        {
            m_intError = 0; m_strError = "";
            bool bCloseConnection = false;
            if (_OracleADO.ConnectionDisposed ||
                _OracleADO.m_Connection == null ||
                (_OracleADO.m_Connection != null &&
                 _OracleADO.m_Connection.State != ConnectionState.Open))
            {
                OracleConnect();
                bCloseConnection = true;
            }
            if (_OracleADO.m_intError == 0)
            {
                try
                {
                    _OracleADO.SqlNonQuery(_OracleADO.m_Connection, p_strSQL);

                }
                catch (Exception e)
                {
                    m_intError = -1;
                    m_strError = e.Message;
                    if (DisplayErrors)
                        MessageBox.Show("!!Error!! \n" +
                            "Module - FCSEntities:Delete_BIOSUM_VOLUME  \n" +
                            "Err Msg - " + e.Message.ToString().Trim(),
                            "BIOSUM", System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Exclamation);

                }
                finally
                {
                    if (bCloseConnection)
                    {
                        //close connection and release resources
                        OracleADO.CloseAndDisposeConnection(_OracleADO.m_Connection, false);

                    }
                }
            }
        }
        /// <summary>
        /// There are no comments for COMP_BIOSUM_VOLS_BY_UPDATE in the schema.
        /// </summary>
        public virtual void COMP_BIOSUM_VOLS_BY_UPDATE ()
        {
            m_intError = 0; m_strError = "";
            bool bCloseConnection = false;
            if (_OracleADO.ConnectionDisposed || 
                _OracleADO.m_Connection == null || 
                (_OracleADO.m_Connection != null && 
                 _OracleADO.m_Connection.State != ConnectionState.Open))
            {
                OracleConnect();
                bCloseConnection = true;
            }
            if (_OracleADO.m_intError == 0)
            {
			    try 
                {
                    //using (Oracle.DataAccess.Client.OracleCommand command = new Oracle.DataAccess.Client.OracleCommand())
                    using (System.Data.OracleClient.OracleCommand command = new System.Data.OracleClient.OracleCommand())
                    {
                        command.Connection = _OracleADO.m_Connection;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = FCSOracle.FCSSchema +  ".COMPUTE_BIOSUM_VOLUMES_PKG.COMP_BIOSUM_VOLS_BY_UPDATE";
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    m_intError = -1;
                    m_strError = e.Message;
                    if (DisplayErrors)
                        MessageBox.Show("!!Error!! \n" +
                            "Module - FCSEntities:COMP_BIOSUM_VOLS_BY_UPDATE  \n" +
                            "Err Msg - " + e.Message.ToString().Trim(),
                            "BIOSUM", System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Exclamation);

                }
                finally
                {
                    if (bCloseConnection)
                    {
                        //close connection and release resources
                        OracleADO.CloseAndDisposeConnection(_OracleADO.m_Connection, false);

                    }
                }
            }
        }

        public int InsertInto_BIOSUM_VOLUMEStable(List<BIOSUM_VOLUME> p_BIOSUM_VOLUME_LIST)
        {
            bool needClose = false;
            int intChanges = 0;
            double dblCount = 0;
            m_intError = 0; m_strError = "";
            try
            {
                if (_OracleADO.ConnectionDisposed ||
                    _OracleADO.m_Connection == null ||
                    (_OracleADO.m_Connection != null && _OracleADO.m_Connection.State != ConnectionState.Open))
                {
                    //create connection to oracle
                    OracleConnect();
                    needClose = true;
                }
               
                BeginTransaction("BIOSUM_VOLUME", "*", "TRE_CN", 1, "");
                foreach (BIOSUM_VOLUME oItem in p_BIOSUM_VOLUME_LIST)
                {
                    dblCount++;
                    if (oItem != null)
                    {
                       System.Data.DataRow oRow = _OracleADO.m_DataSet.Tables["BIOSUM_VOLUME"].NewRow();
                       rowCopyToBIOSUM_VOLUME(oRow, oItem);
                       _OracleADO.m_DataSet.Tables["BIOSUM_VOLUME"].Rows.Add(oRow);
                        
                    }
                }
                intChanges = CommitChanges("BIOSUM_VOLUME");

               
            }
            catch (Exception err)
            {
                m_intError = -1;
                m_strError = err.Message;
            }
            finally
            {
                if (needClose)
                {
                    //close connection and release resources
                    OracleADO.CloseAndDisposeConnection(OracleADO.m_Connection, false);
                    //OracleADO.CloseConnection(OracleADO.m_Connection);
                }
            }

            return intChanges;

        }
        public static void rowCopyToBIOSUM_VOLUME(System.Data.DataRow p_oRow,BIOSUM_VOLUME p_BIOSUM_VOLUME_ITEM)
        {
            p_oRow["STATECD"] = p_BIOSUM_VOLUME_ITEM.STATECD;
            p_oRow["COUNTYCD"] = p_BIOSUM_VOLUME_ITEM.COUNTYCD;
            p_oRow["PLOT"] = p_BIOSUM_VOLUME_ITEM.PLOT;
            p_oRow["INVYR"] = p_BIOSUM_VOLUME_ITEM.INVYR;
            p_oRow["TREE"] = p_BIOSUM_VOLUME_ITEM.TREE;

            if (p_BIOSUM_VOLUME_ITEM.VOL_LOC_GRP != null &&
                p_BIOSUM_VOLUME_ITEM.VOL_LOC_GRP.Trim().Length > 0)
                p_oRow["VOL_LOC_GRP"] = p_BIOSUM_VOLUME_ITEM.VOL_LOC_GRP.ToString();

            if (p_BIOSUM_VOLUME_ITEM.SPCD != null)
                p_oRow["SPCD"] = p_BIOSUM_VOLUME_ITEM.SPCD;

            if (p_BIOSUM_VOLUME_ITEM.DIA != null)
                p_oRow["DIA"] = Convert.ToDecimal(p_BIOSUM_VOLUME_ITEM.DIA);

            if (p_BIOSUM_VOLUME_ITEM.HT != null)
                p_oRow["HT"] = p_BIOSUM_VOLUME_ITEM.HT;

            if (p_BIOSUM_VOLUME_ITEM.ACTUALHT != null)
                p_oRow["ACTUALHT"] = p_BIOSUM_VOLUME_ITEM.ACTUALHT;

            if (p_BIOSUM_VOLUME_ITEM.CR != null)
                p_oRow["CR"] = p_BIOSUM_VOLUME_ITEM.CR;

            p_oRow["STATUSCD"] = p_BIOSUM_VOLUME_ITEM.STATUSCD;

            if (p_BIOSUM_VOLUME_ITEM.DECAYCD != null)
                p_oRow["DECAYCD"] = p_BIOSUM_VOLUME_ITEM.DECAYCD;

            if (p_BIOSUM_VOLUME_ITEM.TREECLCD != null)
                p_oRow["TREECLCD"] = p_BIOSUM_VOLUME_ITEM.TREECLCD;

            if (p_BIOSUM_VOLUME_ITEM.ROUGHCULL != null)
                p_oRow["ROUGHCULL"] = p_BIOSUM_VOLUME_ITEM.ROUGHCULL;

            if (p_BIOSUM_VOLUME_ITEM.CULL != null)
                p_oRow["CULL"] = p_BIOSUM_VOLUME_ITEM.CULL;

            if (p_BIOSUM_VOLUME_ITEM.TOTAGE != null)
                p_oRow["TOTAGE"] = p_BIOSUM_VOLUME_ITEM.TOTAGE;

            //START: ADDED BIOSUM_VOLUME COLUMNS
            if (p_BIOSUM_VOLUME_ITEM.SITREE != null)
                p_oRow["SITREE"] = p_BIOSUM_VOLUME_ITEM.SITREE;

            if (p_BIOSUM_VOLUME_ITEM.WDLDSTEM != null)
                p_oRow["WDLDSTEM"] = p_BIOSUM_VOLUME_ITEM.WDLDSTEM;

            if (p_BIOSUM_VOLUME_ITEM.UPPER_DIA != null)
                p_oRow["UPPER_DIA"] = p_BIOSUM_VOLUME_ITEM.UPPER_DIA;

            if (p_BIOSUM_VOLUME_ITEM.UPPER_DIA_HT != null)
                p_oRow["UPPER_DIA_HT"] = p_BIOSUM_VOLUME_ITEM.UPPER_DIA_HT;

            if (p_BIOSUM_VOLUME_ITEM.CENTROID_DIA != null)
                p_oRow["CENTROID_DIA"] = p_BIOSUM_VOLUME_ITEM.CENTROID_DIA;

            if (p_BIOSUM_VOLUME_ITEM.CENTROID_DIA_HT_ACTUAL != null)
                p_oRow["CENTROID_DIA_HT_ACTUAL"] = p_BIOSUM_VOLUME_ITEM.CENTROID_DIA_HT_ACTUAL;

            if (p_BIOSUM_VOLUME_ITEM.SAWHT != null)
                p_oRow["SAWHT"] = p_BIOSUM_VOLUME_ITEM.SAWHT;

            if (p_BIOSUM_VOLUME_ITEM.HTDMP != null)
                p_oRow["HTDMP"] = p_BIOSUM_VOLUME_ITEM.HTDMP;

            if (p_BIOSUM_VOLUME_ITEM.BOLEHT != null)
                p_oRow["BOLEHT"] = p_BIOSUM_VOLUME_ITEM.BOLEHT;

            if (p_BIOSUM_VOLUME_ITEM.CULLCF != null)
                p_oRow["CULLCF"] = p_BIOSUM_VOLUME_ITEM.CULLCF;

            if (p_BIOSUM_VOLUME_ITEM.CULL_FLD != null)
                p_oRow["CULL_FLD"] = p_BIOSUM_VOLUME_ITEM.CULL_FLD;

            if (p_BIOSUM_VOLUME_ITEM.CULLDEAD != null)
                p_oRow["CULLDEAD"] = p_BIOSUM_VOLUME_ITEM.CULLDEAD;

            if (p_BIOSUM_VOLUME_ITEM.CULLFORM != null)
                p_oRow["CULLFORM"] = p_BIOSUM_VOLUME_ITEM.CULLFORM;

            if (p_BIOSUM_VOLUME_ITEM.CULLMSTOP != null)
                p_oRow["CULLMSTOP"] = p_BIOSUM_VOLUME_ITEM.CULLMSTOP;

            if (p_BIOSUM_VOLUME_ITEM.CFSND != null)
                p_oRow["CFSND"] = p_BIOSUM_VOLUME_ITEM.CFSND;

            if (p_BIOSUM_VOLUME_ITEM.BFSND != null)
                p_oRow["BFSND"] = p_BIOSUM_VOLUME_ITEM.BFSND;

            if (p_BIOSUM_VOLUME_ITEM.PRECIPITATION != null)
                p_oRow["PRECIPITATION"] = p_BIOSUM_VOLUME_ITEM.PRECIPITATION;

            if (p_BIOSUM_VOLUME_ITEM.BALIVE != null)
                p_oRow["BALIVE"] = p_BIOSUM_VOLUME_ITEM.BALIVE;

            if (p_BIOSUM_VOLUME_ITEM.DIAHTCD != null)
                p_oRow["DIAHTCD"] = p_BIOSUM_VOLUME_ITEM.DIAHTCD;

            if (p_BIOSUM_VOLUME_ITEM.STANDING_DEAD_CD != null)
                p_oRow["STANDING_DEAD_CD"] = p_BIOSUM_VOLUME_ITEM.STANDING_DEAD_CD;

            if (p_BIOSUM_VOLUME_ITEM.VOLCFSND_CALC != null)
                p_oRow["VOLCFSND_CALC"] = p_BIOSUM_VOLUME_ITEM.VOLCFSND_CALC;

            if (p_BIOSUM_VOLUME_ITEM.DRYBIO_BOLE_CALC != null)
                p_oRow["DRYBIO_BOLE_CALC"] = p_BIOSUM_VOLUME_ITEM.DRYBIO_BOLE_CALC;

            if (p_BIOSUM_VOLUME_ITEM.DRYBIO_TOP_CALC != null)
                p_oRow["DRYBIO_TOP_CALC"] = p_BIOSUM_VOLUME_ITEM.DRYBIO_TOP_CALC;

            if (p_BIOSUM_VOLUME_ITEM.DRYBIO_SAPLING_CALC != null)
                p_oRow["DRYBIO_SAPLING_CALC"] = p_BIOSUM_VOLUME_ITEM.DRYBIO_SAPLING_CALC;

            if (p_BIOSUM_VOLUME_ITEM.DRYBIO_WDLD_SPP_CALC != null)
                p_oRow["DRYBIO_WDLD_SPP_CALC"] = p_BIOSUM_VOLUME_ITEM.DRYBIO_WDLD_SPP_CALC;

            if (p_BIOSUM_VOLUME_ITEM.CULLBF != null)
                p_oRow["CULLBF"] = p_BIOSUM_VOLUME_ITEM.CULLBF;

            if (p_BIOSUM_VOLUME_ITEM.SUBP != null)
                p_oRow["SUBP"] = p_BIOSUM_VOLUME_ITEM.SUBP;

            if (p_BIOSUM_VOLUME_ITEM.FORMCL != null)
                p_oRow["FORMCL"] = p_BIOSUM_VOLUME_ITEM.FORMCL;
            //END: ADDED BIOSUM_VOLUME COLUMNS

            p_oRow["TRE_CN"] = p_BIOSUM_VOLUME_ITEM.TRE_CN;
            p_oRow["PLT_CN"] = p_BIOSUM_VOLUME_ITEM.PLT_CN;
            p_oRow["CND_CN"] = p_BIOSUM_VOLUME_ITEM.CND_CN;

            

        }
        private void BeginTransaction(string p_strTableName, string p_strColumns, string p_strKeyField, int p_intMAXRecords, string p_strWhereCondition)
        {
            _OracleADO.m_Transaction = _OracleADO.m_Connection.BeginTransaction();
            _OracleADO.InitializeDataAdapter(FCSOracle.FCSSchema, p_strTableName, p_strColumns, p_strKeyField, p_intMAXRecords, p_strWhereCondition);
            _OracleADO.m_DataAdapter.AcceptChangesDuringUpdate = false;
            _OracleADO.m_DataAdapter.ContinueUpdateOnError = false;

        }

        public void SaveChanges()
        {
            CommitChanges("BIOSUM_VOLUME");
        }
        private int CommitChanges(string p_strTableName)
        {
            int x;
            int intChanges = 0;
            m_strError = "";
            m_intError = 0;


            try
            {
                //
                //COMMIT CHANGES TO REMOTE DATA
                //
                for (x = 0; x < 5; x++)
                {
                    System.Data.DataTable oChanges = _OracleADO.m_DataSet.Tables[p_strTableName].GetChanges();
                    if (oChanges != null)
                    {
                        if (oChanges.HasErrors)
                        {
                            _OracleADO.m_DataSet.Tables[p_strTableName].RejectChanges();
                        }
                        else
                        {
                            if (oChanges.Rows.Count > 0)
                            {
                                _OracleADO.m_DataAdapter.Update(_OracleADO.m_DataSet.Tables[p_strTableName]);
                                _OracleADO.m_Transaction.Commit();
                                _OracleADO.m_DataSet.Tables[p_strTableName].AcceptChanges();
                                intChanges = oChanges.Rows.Count;
                            }
                        }
                        break;
                    }
                    else
                    {
                        if (x == 4)
                        {
                            m_intError = -1;
                            m_strError = "Problem saving changes- Reject Changes";
                            _OracleADO.m_DataSet.Tables[p_strTableName].RejectChanges();
                            break;
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(10000);
                        }
                    }
                }
            }
            //catch (Oracle.DataAccess.Client.OracleException errOracleADO)
            catch (System.Data.OracleClient.OracleException errOracleADO)
            {
                m_intError = -1;
                m_strError = errOracleADO.Message;
                MessageBox.Show(errOracleADO.Message);
            }
            catch (Exception err)
            {
                m_intError = -1;
                m_strError = err.Message;
                MessageBox.Show(err.Message);
            }
            finally
            {
                _OracleADO.ResetTransactionObjectToDataAdapter();

            }
            return intChanges;
        }
        public List<BIOSUM_VOLUME> ExecuteSelectSQL_CreateBIOSUM_VOLUMEList(string p_strSQL)
        {
            List<BIOSUM_VOLUME> oList = new List<BIOSUM_VOLUME>();
            bool bCloseConnection = false;
            if (this._OracleADO.ConnectionDisposed ||
                this._OracleADO.m_Connection == null ||
                this._OracleADO.m_Connection.State != System.Data.ConnectionState.Open)
            {
                _OracleADO.OpenConnection(FCSOracle.FCSConnectionString);
                bCloseConnection = true;
            }

            _OracleADO.SqlQueryReader(_OracleADO.m_Connection, p_strSQL);
            if (_OracleADO.m_intError == 0 && _OracleADO.m_DataReader.HasRows)
            {
                while (_OracleADO.m_DataReader.Read())
                {
                    if (_OracleADO.m_DataReader != null)
                    {
                        BIOSUM_VOLUME oNew = new BIOSUM_VOLUME();

                        oNew.STATECD = Convert.ToInt32(_OracleADO.m_DataReader["STATECD"]);
                        oNew.COUNTYCD = Convert.ToInt32(_OracleADO.m_DataReader["COUNTYCD"]);
                        oNew.PLOT = Convert.ToInt32(_OracleADO.m_DataReader["PLOT"]);
                        oNew.INVYR = Convert.ToInt32(_OracleADO.m_DataReader["INVYR"]);
                        oNew.TREE =  Convert.ToInt32(_OracleADO.m_DataReader["TREE"]);

                        if (_OracleADO.m_DataReader["VOL_LOC_GRP"] != DBNull.Value)
                           oNew.VOL_LOC_GRP =  _OracleADO.m_DataReader["VOL_LOC_GRP"].ToString();

                        if (_OracleADO.m_DataReader["SPCD"] != DBNull.Value)
                            oNew.SPCD =  Convert.ToInt32(_OracleADO.m_DataReader["SPCD"]);

                        if (_OracleADO.m_DataReader["DIA"] != DBNull.Value)
                            oNew.DIA = Convert.ToDouble(_OracleADO.m_DataReader["DIA"]);

                        if (_OracleADO.m_DataReader["HT"] != DBNull.Value)
                            oNew.HT = Convert.ToInt32(_OracleADO.m_DataReader["HT"]);

                        if (_OracleADO.m_DataReader["ACTUALHT"] != DBNull.Value)
                            oNew.ACTUALHT = Convert.ToInt32(_OracleADO.m_DataReader["ACTUALHT"]);

                        if (_OracleADO.m_DataReader["CR"] != DBNull.Value)
                            oNew.ACTUALHT = Convert.ToInt32(_OracleADO.m_DataReader["CR"]);

                        oNew.STATUSCD = Convert.ToInt16(_OracleADO.m_DataReader["STATUSCD"]);

                        if ( _OracleADO.m_DataReader["DECAYCD"] != DBNull.Value)
                            oNew.DECAYCD = Convert.ToInt32(_OracleADO.m_DataReader["DECAYCD"]);

                        if (_OracleADO.m_DataReader["TREECLCD"] != DBNull.Value)
                           oNew.TREECLCD = Convert.ToInt32(_OracleADO.m_DataReader["TREECLCD"]);

                        if (_OracleADO.m_DataReader["ROUGHCULL"] != DBNull.Value)
                           oNew.ROUGHCULL = Convert.ToInt32(_OracleADO.m_DataReader["ROUGHCULL"]);

                        if (_OracleADO.m_DataReader["CULL"] != DBNull.Value)
                            oNew.CULL = Convert.ToInt32(_OracleADO.m_DataReader["CULL"]);

                        if (_OracleADO.m_DataReader["TOTAGE"] != DBNull.Value)
                            oNew.TOTAGE = Convert.ToDouble(_OracleADO.m_DataReader["TOTAGE"]);

                        //START: ADDED BIOSUM_VOLUME COLUMNS
                        if (_OracleADO.m_DataReader["SITREE"] != DBNull.Value)
                            oNew.SITREE = Convert.ToInt32(_OracleADO.m_DataReader["SITREE"]);

                        if (_OracleADO.m_DataReader["WDLDSTEM"] != DBNull.Value)
                            oNew.WDLDSTEM = Convert.ToInt32(_OracleADO.m_DataReader["WDLDSTEM"]);

                        if (_OracleADO.m_DataReader["UPPER_DIA"] != DBNull.Value)
                            oNew.UPPER_DIA = Convert.ToDouble(_OracleADO.m_DataReader["UPPER_DIA"]);

                        if (_OracleADO.m_DataReader["UPPER_DIA_HT"] != DBNull.Value)
                            oNew.UPPER_DIA_HT = Convert.ToDouble(_OracleADO.m_DataReader["UPPER_DIA_HT"]);

                        if (_OracleADO.m_DataReader["CENTROID_DIA"] != DBNull.Value)
                            oNew.CENTROID_DIA = Convert.ToDouble(_OracleADO.m_DataReader["CENTROID_DIA"]);

                        if (_OracleADO.m_DataReader["CENTROID_DIA_HT_ACTUAL"] != DBNull.Value)
                            oNew.CENTROID_DIA_HT_ACTUAL = Convert.ToDouble(_OracleADO.m_DataReader["CENTROID_DIA_HT_ACTUAL"]);

                        if (_OracleADO.m_DataReader["SAWHT"] != DBNull.Value)
                            oNew.SAWHT = Convert.ToInt32(_OracleADO.m_DataReader["SAWHT"]);

                        if (_OracleADO.m_DataReader["HTDMP"] != DBNull.Value)
                            oNew.HTDMP = Convert.ToDouble(_OracleADO.m_DataReader["HTDMP"]);

                        if (_OracleADO.m_DataReader["BOLEHT"] != DBNull.Value)
                            oNew.BOLEHT = Convert.ToInt32(_OracleADO.m_DataReader["BOLEHT"]);

                        if (_OracleADO.m_DataReader["CULLCF"] != DBNull.Value)
                            oNew.CULLCF = Convert.ToInt32(_OracleADO.m_DataReader["CULLCF"]);

                        if (_OracleADO.m_DataReader["CULL_FLD"] != DBNull.Value)
                            oNew.CULL_FLD = Convert.ToInt32(_OracleADO.m_DataReader["CULL_FLD"]);

                        if (_OracleADO.m_DataReader["CULLDEAD"] != DBNull.Value)
                            oNew.CULLDEAD = Convert.ToInt32(_OracleADO.m_DataReader["CULLDEAD"]);

                        if (_OracleADO.m_DataReader["CULLFORM"] != DBNull.Value)
                            oNew.CULLFORM = Convert.ToInt32(_OracleADO.m_DataReader["CULLFORM"]);

                        if (_OracleADO.m_DataReader["CULLMSTOP"] != DBNull.Value)
                            oNew.CULLMSTOP = Convert.ToInt32(_OracleADO.m_DataReader["CULLMSTOP"]);

                        if (_OracleADO.m_DataReader["CFSND"] != DBNull.Value)
                            oNew.CFSND = Convert.ToInt32(_OracleADO.m_DataReader["CFSND"]);

                        if (_OracleADO.m_DataReader["BFSND"] != DBNull.Value)
                            oNew.BFSND = Convert.ToInt32(_OracleADO.m_DataReader["BFSND"]);

                        if (_OracleADO.m_DataReader["PRECIPITATION"] != DBNull.Value)
                            oNew.PRECIPITATION = Convert.ToDouble(_OracleADO.m_DataReader["PRECIPITATION"]);

                        if (_OracleADO.m_DataReader["BALIVE"] != DBNull.Value)
                            oNew.BALIVE = Convert.ToDouble(_OracleADO.m_DataReader["BALIVE"]);

                        if (_OracleADO.m_DataReader["DIAHTCD"] != DBNull.Value)
                            oNew.DIAHTCD = Convert.ToInt32(_OracleADO.m_DataReader["DIAHTCD"]);

                        if (_OracleADO.m_DataReader["STANDING_DEAD_CD"] != DBNull.Value)
                            oNew.STANDING_DEAD_CD = Convert.ToInt32(_OracleADO.m_DataReader["STANDING_DEAD_CD"]);

                        if (_OracleADO.m_DataReader["VOLCFSND_CALC"] != DBNull.Value)
                            oNew.VOLCFSND_CALC = Convert.ToDouble(_OracleADO.m_DataReader["VOLCFSND_CALC"]);

                        if (_OracleADO.m_DataReader["DRYBIO_BOLE_CALC"] != DBNull.Value)
                            oNew.DRYBIO_BOLE_CALC = Convert.ToDouble(_OracleADO.m_DataReader["DRYBIO_BOLE_CALC"]);

                        if (_OracleADO.m_DataReader["DRYBIO_TOP_CALC"] != DBNull.Value)
                            oNew.DRYBIO_TOP_CALC = Convert.ToDouble(_OracleADO.m_DataReader["DRYBIO_TOP_CALC"]);

                        if (_OracleADO.m_DataReader["DRYBIO_SAPLING_CALC"] != DBNull.Value)
                            oNew.DRYBIO_SAPLING_CALC = Convert.ToDouble(_OracleADO.m_DataReader["DRYBIO_SAPLING_CALC"]);

                        if (_OracleADO.m_DataReader["DRYBIO_WDLD_SPP_CALC"] != DBNull.Value)
                            oNew.DRYBIO_WDLD_SPP_CALC = Convert.ToDouble(_OracleADO.m_DataReader["DRYBIO_WDLD_SPP_CALC"]);

                        if (_OracleADO.m_DataReader["CULLBF"] != DBNull.Value)
                            oNew.CULLBF = Convert.ToInt32(_OracleADO.m_DataReader["CULLBF"]);

                        if (_OracleADO.m_DataReader["SUBP"] != DBNull.Value)
                            oNew.SUBP = Convert.ToByte(_OracleADO.m_DataReader["SUBP"]);
                        //END: ADDED BIOSUM_VOLUME COLUMNS

                        oNew.TRE_CN =  _OracleADO.m_DataReader["TRE_CN"].ToString();
                        oNew.PLT_CN =  _OracleADO.m_DataReader["PLT_CN"].ToString();
                        oNew.CND_CN = _OracleADO.m_DataReader["CND_CN"].ToString();

                        if (_OracleADO.m_DataReader["VOLCFGRS_CALC"] != DBNull.Value)
                            oNew.VOLCFGRS_CALC = Convert.ToDouble(_OracleADO.m_DataReader["VOLCFGRS_CALC"]);

                        if (_OracleADO.m_DataReader["VOLCSGRS_CALC"] != DBNull.Value)
                            oNew.VOLCSGRS_CALC = Convert.ToDouble(_OracleADO.m_DataReader["VOLCSGRS_CALC"]);

                        if (_OracleADO.m_DataReader["VOLCFNET_CALC"] != DBNull.Value)
                            oNew.VOLCFNET_CALC = Convert.ToDouble(_OracleADO.m_DataReader["VOLCFNET_CALC"]);

                        if (_OracleADO.m_DataReader["DRYBIOM_CALC"] != DBNull.Value)
                            oNew.DRYBIOM_CALC = Convert.ToDouble(_OracleADO.m_DataReader["DRYBIOM_CALC"]);

                        if (_OracleADO.m_DataReader["DRYBIOT_CALC"] != DBNull.Value)
                            oNew.DRYBIOT_CALC = Convert.ToDouble(_OracleADO.m_DataReader["DRYBIOT_CALC"]);

                        if (_OracleADO.m_DataReader["VOLTSGRS_CALC"] != DBNull.Value)
                            oNew.VOLTSGRS_CALC = Convert.ToDouble(_OracleADO.m_DataReader["VOLTSGRS_CALC"]);
                        oList.Add(oNew);
                        oNew = null;

                    }
                }
                _OracleADO.m_DataReader.Close();
                _OracleADO.m_DataReader.Dispose();
                _OracleADO.m_DataReader = null;
            }
            if (bCloseConnection)
            {
                _OracleADO.CloseAndDisposeConnection(_OracleADO.m_Connection, false);
            }
            if (oList.Count == 0) return null;
            else return oList;

        }
        public static void AddBiosumVolumeColumns(string p_strTempDir,string p_strAppDir,out string p_strBATFile)
        {
            p_strBATFile = "";
            bool bFound = true;
            string[,] strAlterCommands =
            {
                {"BALIVE", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD BALIVE NUMBER(9,4);"},
                {"BFSND", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD BFSND NUMBER(3);"},
                {"BOLEHT", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD BOLEHT NUMBER(3);"},
                {"CENTROID_DIA", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CENTROID_DIA NUMBER(4,1);"},
                {"CENTROID_DIA_HT_ACTUAL", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CENTROID_DIA_HT_ACTUAL NUMBER(4,1);"},
                {"CFSND", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CFSND NUMBER(3);"},
                {"CULLBF", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CULLBF NUMBER(3);"},
                {"CULLCF", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CULLCF NUMBER(3);"},
                {"CULLDEAD", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CULLDEAD NUMBER(3);"},
                {"CULLFORM", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CULLFORM NUMBER(3);"},
                {"CULLMSTOP", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CULLMSTOP NUMBER(3);"},
                {"CULL_FLD", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD CULL_FLD NUMBER(2);"},
                {"DIAHTCD", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD DIAHTCD NUMBER(1);"},
                {"DRYBIO_BOLE_CALC", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD DRYBIO_BOLE_CALC NUMBER(13,6);"},
                {"DRYBIO_SAPLING_CALC", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD DRYBIO_SAPLING_CALC NUMBER(13,6);"},
                {"DRYBIO_TOP_CALC", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD DRYBIO_TOP_CALC NUMBER(13,6);"},
                {"DRYBIO_WDLD_SPP_CALC", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD DRYBIO_WDLD_SPP_CALC NUMBER(13,6);"},
                {"FORMCL", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD FORMCL NUMBER(1);"},
                {"HTDMP", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD HTDMP NUMBER(3,1);"},
                {"PRECIPITATION", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD PRECIPITATION NUMBER;"},
                {"SAWHT", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD SAWHT NUMBER(2);"},
                {"SITREE", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD SITREE NUMBER(3,1);"},
                {"STANDING_DEAD_CD", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD STANDING_DEAD_CD NUMBER(1);"},
                {"SUBP", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD SUBP NUMBER(1);"},
                {"UPPER_DIA", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD UPPER_DIA NUMBER(4,1);"},
                {"UPPER_DIA_HT", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD UPPER_DIA_HT NUMBER(4,1);"},
                {"VOLCFSND_CALC", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD VOLCFSND_CALC NUMBER(13,6);"},
                {"WDLDSTEM", "ALTER TABLE FCS_BIOSUM.BIOSUM_VOLUME ADD WDLDSTEM NUMBER(3);"},
            };
            Oracle.ADO.FCSOracle fcs = new Oracle.ADO.FCSOracle();
            string strConn = fcs.getOracleConnString("XE", "fcs_biosum", "fcs");
            fcs.DisplayErrors = false;
            fcs.OpenConnection(strConn);
            if (fcs.m_intError == 0)
            {

                string strColumns = fcs.getFieldNames(fcs.m_Connection, "SELECT * FROM FCS_BIOSUM.BIOSUM_VOLUME WHERE 1=2");
                fcs.CloseAndDisposeConnection(fcs.m_Connection, true);
                if (strColumns != null && strColumns.Trim().Length > 0)
                {
                    strColumns = "," + strColumns + ",";
                    for (int x = 0; x <= strAlterCommands.GetLength(0)-1; x++)
                    {
                        if (strColumns.ToUpper().IndexOf("," + strAlterCommands[x, 0] + ",", 0) < 0)
                        {
                            bFound = false;
                        }
                        else
                        {
                            strAlterCommands[x, 0] = "FOUND";
                        }
                    }
                    if (!bFound)
                    {
                        string strFile = p_strTempDir + @"\FCS_BIOSUM_ModifyBiomassColumns.SQL";
                        if (System.IO.File.Exists(strFile)) System.IO.File.Delete(strFile);
                        System.Threading.Thread.Sleep(1000);
                        WriteText(strFile, "CONNECT FCS_BIOSUM/fcs;\r\n");
                        WriteText(strFile, "WHENEVER SQLERROR CONTINUE NONE\r\n");
                        WriteText(strFile, "DELETE FROM FCS_BIOSUM.BIOSUM_VOLUME;\r\n");
                        WriteText(strFile, "COMMIT;\r\n");
                        for (int x = 0; x <= strAlterCommands.GetLength(0)-1; x++)
                        {
                            if (strAlterCommands[x, 0] != "FOUND")
                            {
                                WriteText(strFile, strAlterCommands[x, 1] + "\r\n");
                            }

                        }
                        WriteText(strFile, "COMMIT;\r\n");
                        WriteText(strFile, "EXIT\r\n");
                        System.Threading.Thread.Sleep(1000);

                        p_strBATFile = p_strTempDir + @"\FCS_BIOSUM_ModifyBiomassColumns.BAT";
                        System.IO.File.Copy(p_strAppDir + "\\fcs\\FCS_BIOSUM_ModifyBiomassColumns.BAT", p_strBATFile, true);
                    }
                }
            }

            fcs = null;
        }
        private static void WriteText(string p_strTextFile, string p_strText)
        {
            System.IO.FileStream oTextFileStream;
            System.IO.StreamWriter oTextStreamWriter;

            if (!System.IO.File.Exists(p_strTextFile))
            {
                oTextFileStream = new System.IO.FileStream(p_strTextFile, System.IO.FileMode.Create,
                    System.IO.FileAccess.Write);
            }
            else
            {
                oTextFileStream = new System.IO.FileStream(p_strTextFile, System.IO.FileMode.Append,
                    System.IO.FileAccess.Write);
            }

            oTextStreamWriter = new System.IO.StreamWriter(oTextFileStream);
            oTextStreamWriter.Write(p_strText);
            oTextStreamWriter.Close();
            oTextFileStream.Close();
        }



        #endregion
    }

    #endregion
}

namespace FcsClassLibrary
{

    /// <summary>
    /// There are no comments for FcsClassLibrary.BIOSUM_VOLUME in the schema.
    /// </summary>
    public partial class BIOSUM_VOLUME    {

        public BIOSUM_VOLUME()
        {
        }

        #region Properties
        public virtual int? ACTUALHT { get; set; }
        public virtual double? BALIVE { get; set; }
        public virtual int? BFSND { get; set; }
        public virtual int? BOLEHT { get; set; }
        public virtual double? CENTROID_DIA { get; set; }
        public virtual double? CENTROID_DIA_HT_ACTUAL { get; set; }
        public virtual int? CFSND { get; set; }
        public virtual string CND_CN { get; set; }
        public virtual int COUNTYCD { get; set; }
        public virtual int? CR { get; set; }
        public virtual int? CULL { get; set; }
        public virtual int? CULL_FLD { get; set; }
        public virtual int? CULLBF { get; set; }
        public virtual int? CULLCF { get; set; }
        public virtual int? CULLDEAD { get; set; }
        public virtual int? CULLFORM { get; set; }
        public virtual int? CULLMSTOP { get; set; }
        public virtual int? DECAYCD { get; set; }
        public virtual double? DIA { get; set; }
        public virtual int? DIAHTCD { get; set; }
        public virtual double? DRYBIO_BOLE_CALC { get; set; }
        public virtual double? DRYBIO_SAPLING_CALC { get; set; }
        public virtual double? DRYBIO_TOP_CALC { get; set; }
        public virtual double? DRYBIO_WDLD_SPP_CALC { get; set; }
        public virtual double? DRYBIOM_CALC { get; set; }
        public virtual double? DRYBIOT_CALC { get; set; }
        public virtual byte? FORMCL { get; set; }
        public virtual int? HT { get; set; }
        public virtual double? HTDMP { get; set; }
        public virtual int INVYR { get; set; }
        public virtual int PLOT { get; set; }
        public virtual string PLT_CN { get; set; }
        public virtual double? PRECIPITATION { get; set; }
        public virtual int? ROUGHCULL { get; set; }
        public virtual int? SAWHT { get; set; }
        public virtual int? SITREE { get; set; }
        public virtual decimal? SPCD { get; set; }
        public virtual int? STANDING_DEAD_CD { get; set; }
        public virtual int STATECD { get; set; }
        public virtual short STATUSCD { get; set; }
        public virtual byte? SUBP { get; set; }
        public virtual double? TOTAGE { get; set; }
        public virtual string TRE_CN { get; set; }
        public virtual int TREE { get; set; }
        public virtual int? TREECLCD { get; set; }
        public virtual double? UPPER_DIA { get; set; }
        public virtual double? UPPER_DIA_HT { get; set; }
        public virtual string VOL_LOC_GRP { get; set; }
        public virtual double? VOLCFGRS_CALC { get; set; }
        public virtual double? VOLCFNET_CALC { get; set; }
        public virtual double? VOLCFSND_CALC { get; set; }
        public virtual double? VOLCSGRS_CALC { get; set; }
        public virtual double? VOLTSGRS_CALC { get; set; }
        public virtual int? WDLDSTEM { get; set; }
        #endregion
    }

}
