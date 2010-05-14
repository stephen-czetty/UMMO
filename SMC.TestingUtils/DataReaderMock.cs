﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SMC.TestingUtils
{
    public class DataReaderMock : IDataReader
    {
        readonly IList< IList< IList< object > > > _recordsToRetrieve;
        readonly IList< IList< string > > _recordSetColumnNames;
        int _recordsetNumber;
        int _rowNumber;
        DataTable _schemaTable;
        bool _readyForPlayback;

        [Obsolete("Use the parameterless constructor and AddRecordSet/AddRow methods.")]
        public DataReaderMock( params IList< KeyValuePair< string, object > >[] recordsToRetrieve )
        {
            _recordsToRetrieve = new List< IList< IList< object > > >();
            _recordSetColumnNames = new List< IList< string > >();
            _recordsetNumber = 0;
            _rowNumber = -1;

            foreach ( var record in recordsToRetrieve )
                AddRecordSet( record.Select( x => x.Key ).ToArray() )
                    .AddRow( record.Select( x => x.Value ).ToArray() );

            Playback();
        }

        public DataReaderMock()
        {
            _recordsToRetrieve = new List< IList< IList< object > > >();
            _recordSetColumnNames = new List< IList< string > >();
            _recordsetNumber = 0;
            _rowNumber = -1;
            _readyForPlayback = false;
        }

        public DataReaderMock AddRecordSet(params string[] columnNames)
        {
            ThrowIfInPlaybackMode();

            _recordSetColumnNames.Add( columnNames );
            _recordsToRetrieve.Add( new List< IList< object > >() );

            return this;
        }

        public DataReaderMock AddRow(params object[] columnValues)
        {
            ThrowIfInPlaybackMode();

            if (_recordsToRetrieve.Count == 0)
                throw new InvalidOperationException( "Attempt to add a row before defining a recordset" );

            _recordsToRetrieve.Last().Add( columnValues );  

            return this;
        }

        public DataReaderMock Playback()
        {
            _readyForPlayback = true;

            return this;
        }

        void ThrowIfInPlaybackMode()
        {
            if (_readyForPlayback)
                throw new InvalidOperationException( "Cannot add records while in Playback state." );
        }

        void ThrowUnlessInPlaybackMode()
        {
            if (!_readyForPlayback)
                throw new InvalidOperationException( "Cannot execute outside of Playback state." );
        }

        #region IDataReader Members

        public void Dispose()
        {
            ThrowUnlessInPlaybackMode();
            IsClosed = true;
        }


        public string GetName( int i )
        {
            ThrowUnlessInPlaybackMode();

            return _recordSetColumnNames[_recordsetNumber][i];
        }


        public string GetDataTypeName( int i )
        {
            ThrowUnlessInPlaybackMode();

            return _recordsToRetrieve[_recordsetNumber][0][i].GetType().Name;
        }


        public Type GetFieldType( int i )
        {
            ThrowUnlessInPlaybackMode();

            return _recordsToRetrieve[_recordsetNumber][0][i].GetType();
        }


        public object GetValue( int i )
        {
            ThrowUnlessInPlaybackMode();

            return GetColumnValue( i );
        }


        public int GetValues( object[] values )
        {
            ThrowUnlessInPlaybackMode();

            for ( int i = 0; i < values.Length; i++ )
            {
                if ( i >= _recordSetColumnNames[_recordsetNumber].Count )
                    break;

                values[i] = GetColumnValue( i );
            }

            return Math.Min( values.Length, _recordSetColumnNames[_recordsetNumber].Count );
        }


        public int GetOrdinal( string name )
        {
            ThrowUnlessInPlaybackMode();

            for (int i = 0; i < _recordSetColumnNames[_recordsetNumber].Count; i++)
                if (_recordSetColumnNames[_recordsetNumber][i] == name)
                    return i;

            throw new IndexOutOfRangeException();
        }


        public bool GetBoolean( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (bool)GetColumnValue( i );
        }


        public byte GetByte( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (byte)GetColumnValue( i );
        }


        public long GetBytes( int i, long fieldOffset, byte[] buffer, int bufferoffset, int length )
        {
            ThrowUnlessInPlaybackMode();

            if (fieldOffset < 0)
                return 0;

            var str = Encoding.Default.GetBytes( (string)GetColumnValue( i ) );
            long k = 0;
            for (long j = fieldOffset; j < str.Length && k < length; j++, k++)
                buffer[bufferoffset++] = str[j];

            return k;
        }


        public char GetChar( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (char)GetColumnValue( i );
        }


        public long GetChars( int i, long fieldoffset, char[] buffer, int bufferoffset, int length )
        {
            ThrowUnlessInPlaybackMode();

            if (fieldoffset < 0)
                return 0;

            var str = ((string)GetColumnValue( i )).ToCharArray();
            long k = 0;
            for ( long j = fieldoffset; j < str.Length && k < length; j++,k++ )
                buffer[bufferoffset++] = str[j];

            return k;
        }


        public Guid GetGuid( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (Guid)GetColumnValue( i );
        }

        object GetColumnValue( int columnOrdinal )
        {
            ThrowUnlessInPlaybackMode();

            return _recordsToRetrieve[_recordsetNumber][_rowNumber][columnOrdinal];
        }


        public short GetInt16( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (short)GetColumnValue( i );
        }


        public int GetInt32( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (int)GetColumnValue( i );
        }


        public long GetInt64( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (long)GetColumnValue( i );
        }


        public float GetFloat( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (float)GetColumnValue( i );
        }


        public double GetDouble( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (double)GetColumnValue( i );
        }


        public string GetString( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (string)GetColumnValue( i );
        }


        public decimal GetDecimal( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (decimal)GetColumnValue( i );
        }


        public DateTime GetDateTime( int i )
        {
            ThrowUnlessInPlaybackMode();

            return (DateTime)GetColumnValue( i );
        }


        public IDataReader GetData( int i )
        {
            ThrowUnlessInPlaybackMode();

            return
                new DataReaderMock()
                    .AddRecordSet( _recordSetColumnNames[_recordsetNumber][i] )
                    .AddRow( GetColumnValue( i ) )
                    .Playback();
        }


        public bool IsDBNull( int i )
        {
            ThrowUnlessInPlaybackMode();

            return GetColumnValue( i ) == null;
        }


        public int FieldCount
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return _recordSetColumnNames[_recordsetNumber].Count;
            }
        }


        object IDataRecord.this[ int i ]
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return GetColumnValue( i );
            }
        }


        object IDataRecord.this[string name]
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return GetColumnValue( GetOrdinal( name ) );
            }
        }


        public void Close()
        {
            ThrowUnlessInPlaybackMode();

            IsClosed = true;
        }

        // TODO: Add the rest of the columns to duplicate what SqlDataReader returns
        public DataTable GetSchemaTable()
        {
            ThrowUnlessInPlaybackMode();

            if ( _schemaTable == null )
            {
                _schemaTable = new DataTable();
                _schemaTable.Columns.Add( "ColumnName" );
                _schemaTable.Columns.Add( "ColumnOrdinal", typeof(int) );
                _schemaTable.Columns.Add( "DataType", typeof(Type) );

                for ( int i = 0; i < _recordsToRetrieve[_recordsetNumber].Count; i++ )
                {
                    DataRow newRow = _schemaTable.NewRow();
                    newRow["ColumnName"] = _recordSetColumnNames[_recordsetNumber][i];
                    newRow["ColumnOrdinal"] = i;
                    newRow["DataType"] = GetColumnValue( i ).GetType();

                    _schemaTable.Rows.Add( newRow );
                }
            }

            return _schemaTable;
        }


        public bool NextResult()
        {
            ThrowUnlessInPlaybackMode();

            _rowNumber = -1;
            _schemaTable = null;
            return (++_recordsetNumber < _recordsToRetrieve.Count);
        }


        public bool Read()
        {
            ThrowUnlessInPlaybackMode();

            return ++_rowNumber < _recordsToRetrieve[_recordsetNumber].Count;
        }


        public int Depth
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return 0;
            }
        }

        bool _isClosed;
        public bool IsClosed
        {
            get
            {
                ThrowUnlessInPlaybackMode();
                return _isClosed;
            }
            private set { _isClosed = value; }
        }

        public int RecordsAffected
        {
            get
            {
                ThrowUnlessInPlaybackMode();

                if (IsClosed)
                    return -1;
                return 0;
            }
        }

        #endregion
    }
}
