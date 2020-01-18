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
                        command.CommandText = "FCS_BIOSUM.COMPUTE_BIOSUM_VOLUMES_PKG";
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
                        command.CommandText = "FCS_BIOSUM.COMPUTE_BIOSUM_VOLUMES_PKG.COMP_BIOSUM_VOLS_BY_CURSOR";
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
                    _OracleADO.SqlNonQuery(_OracleADO.m_Connection,"DELETE FROM BIOSUM_VOLUME");

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
                        command.CommandText = "FCS_BIOSUM.COMPUTE_BIOSUM_VOLUMES_PKG.COMP_BIOSUM_VOLS_BY_UPDATE";
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

            p_oRow["TRE_CN"] = p_BIOSUM_VOLUME_ITEM.TRE_CN;
            p_oRow["PLT_CN"] = p_BIOSUM_VOLUME_ITEM.PLT_CN;
            p_oRow["CND_CN"] = p_BIOSUM_VOLUME_ITEM.CND_CN;

            

        }
        private void BeginTransaction(string p_strTableName, string p_strColumns, string p_strKeyField, int p_intMAXRecords, string p_strWhereCondition)
        {
            _OracleADO.m_Transaction = _OracleADO.m_Connection.BeginTransaction();
            _OracleADO.InitializeDataAdapter("FCS_BIOSUM", p_strTableName, p_strColumns, p_strKeyField, p_intMAXRecords, p_strWhereCondition);
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
    
        /// <summary>
        /// There are no comments for STATECD in the schema.
        /// </summary>
        public virtual int STATECD
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for COUNTYCD in the schema.
        /// </summary>
        public virtual int COUNTYCD
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for PLOT in the schema.
        /// </summary>
        public virtual int PLOT
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for INVYR in the schema.
        /// </summary>
        public virtual int INVYR
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for TREE in the schema.
        /// </summary>
        public virtual int TREE
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for VOL_LOC_GRP in the schema.
        /// </summary>
        public virtual string VOL_LOC_GRP
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for SPCD in the schema.
        /// </summary>
        public virtual global::System.Nullable<decimal> SPCD
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for DIA in the schema.
        /// </summary>
        public virtual global::System.Nullable<double> DIA
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for HT in the schema.
        /// </summary>
        public virtual global::System.Nullable<int> HT
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for ACTUALHT in the schema.
        /// </summary>
        public virtual global::System.Nullable<int> ACTUALHT
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for CR in the schema.
        /// </summary>
        public virtual global::System.Nullable<int> CR
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for STATUSCD in the schema.
        /// </summary>
        public virtual short STATUSCD
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for DECAYCD in the schema.
        /// </summary>
        public virtual global::System.Nullable<int> DECAYCD
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for TREECLCD in the schema.
        /// </summary>
        public virtual global::System.Nullable<int> TREECLCD
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for ROUGHCULL in the schema.
        /// </summary>
        public virtual global::System.Nullable<int> ROUGHCULL
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for CULL in the schema.
        /// </summary>
        public virtual global::System.Nullable<int> CULL
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for TOTAGE in the schema.
        /// </summary>
        public virtual global::System.Nullable<double> TOTAGE
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for TRE_CN in the schema.
        /// </summary>
        public virtual string TRE_CN
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for PLT_CN in the schema.
        /// </summary>
        public virtual string PLT_CN
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for CND_CN in the schema.
        /// </summary>
        public virtual string CND_CN
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for VOLCFGRS_CALC in the schema.
        /// </summary>
        public virtual global::System.Nullable<double> VOLCFGRS_CALC
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for VOLCSGRS_CALC in the schema.
        /// </summary>
        public virtual global::System.Nullable<double> VOLCSGRS_CALC
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for VOLCFNET_CALC in the schema.
        /// </summary>
        public virtual global::System.Nullable<double> VOLCFNET_CALC
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for DRYBIOM_CALC in the schema.
        /// </summary>
        public virtual global::System.Nullable<double> DRYBIOM_CALC
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for DRYBIOT_CALC in the schema.
        /// </summary>
        public virtual global::System.Nullable<double> DRYBIOT_CALC
        {
            get;
            set;
        }

    
        /// <summary>
        /// There are no comments for VOLTSGRS_CALC in the schema.
        /// </summary>
        public virtual global::System.Nullable<double> VOLTSGRS_CALC
        {
            get;
            set;
        }


        #endregion
    }

}
