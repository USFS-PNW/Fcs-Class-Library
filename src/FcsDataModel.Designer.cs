﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Devart Entity Developer tool using Entity Framework DbContext template.
// Code is generated on: 2/8/2019 1:11:05 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace FcsClassLibrary
{

    #region FCS_Entities
    public partial class FCS_Entities : DbContext
    {
        #region Constructors

        /// <summary>
        /// Initialize a new FCS_Entities object.
        /// </summary>
        public FCS_Entities() :
                base(@"name=FCS_Entities_ConnectionString")
        {
            Configure();
        }

        /// <summary>
        /// Initializes a new FCS_Entities object using the connection string found in the 'FCS_Entities' section of the application configuration file.
        /// </summary>
        public FCS_Entities(string nameOrConnectionString) :
                base(nameOrConnectionString)
        {
            Configure();
        }

        private void Configure()
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = true;
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            throw new UnintentionalCodeFirstException();
        }

    
        /// <summary>
        /// There are no comments for BIOSUM_VOLUME in the schema.
        /// </summary>
        public virtual DbSet<BIOSUM_VOLUME> BIOSUM_VOLUMEs { get; set; }

        #region Methods

    
        /// <summary>
        /// There are no comments for COMPUTE_BIOSUM_VOLUMES_PKG in the schema.
        /// </summary>
        public virtual void COMPUTE_BIOSUM_VOLUMES_PKG ()
        {
            EntityConnection connection = ((IObjectContextAdapter)this).ObjectContext.Connection as EntityConnection;
            bool needClose = false;
            if (connection.State != ConnectionState.Open) {
              connection.Open();
              needClose = true;
            }

			try {
              using(EntityCommand command = new EntityCommand())
              {
                if (((IObjectContextAdapter)this).ObjectContext.CommandTimeout.HasValue)
                  command.CommandTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout.Value;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = @"FCS_Entities.COMPUTE_BIOSUM_VOLUMES_PKG";
                command.Connection = connection;
                command.ExecuteNonQuery();
              }
            }
            finally {
              if (needClose)
                connection.Close();
            }
        }

    
        /// <summary>
        /// There are no comments for COMP_BIOSUM_VOLS_BY_CURSOR in the schema.
        /// </summary>
        public virtual void COMP_BIOSUM_VOLS_BY_CURSOR ()
        {
            EntityConnection connection = ((IObjectContextAdapter)this).ObjectContext.Connection as EntityConnection;
            bool needClose = false;
            if (connection.State != ConnectionState.Open) {
              connection.Open();
              needClose = true;
            }

			try {
              using(EntityCommand command = new EntityCommand())
              {
                if (((IObjectContextAdapter)this).ObjectContext.CommandTimeout.HasValue)
                  command.CommandTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout.Value;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = @"FCS_Entities.COMP_BIOSUM_VOLS_BY_CURSOR";
                command.Connection = connection;
                command.ExecuteNonQuery();
              }
            }
            finally {
              if (needClose)
                connection.Close();
            }
        }

    
        /// <summary>
        /// There are no comments for COMP_BIOSUM_VOLS_BY_UPDATE in the schema.
        /// </summary>
        public virtual void COMP_BIOSUM_VOLS_BY_UPDATE ()
        {
            EntityConnection connection = ((IObjectContextAdapter)this).ObjectContext.Connection as EntityConnection;
            bool needClose = false;
            if (connection.State != ConnectionState.Open) {
              connection.Open();
              needClose = true;
            }

			try {
              using(EntityCommand command = new EntityCommand())
              {
                if (((IObjectContextAdapter)this).ObjectContext.CommandTimeout.HasValue)
                  command.CommandTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout.Value;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = @"FCS_Entities.COMP_BIOSUM_VOLS_BY_UPDATE";
                command.Connection = connection;
                command.ExecuteNonQuery();
              }
            }
            finally {
              if (needClose)
                connection.Close();
            }
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
