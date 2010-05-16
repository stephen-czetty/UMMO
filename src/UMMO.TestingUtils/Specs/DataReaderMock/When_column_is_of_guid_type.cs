﻿using System;
using System.Data;
using Machine.Specifications;

namespace SMC.TestingUtils.Specs.DataReaderMock
{
    [Subject( typeof(TestingUtils.DataReaderMock) )]
    public class When_column_is_of_guid_type : DataReaderMockSpecsWithRecordSetDefined
    {
        static readonly Guid GuidValue = RandomDataGenerator.Guid;
        Because of = () => SetupTestRecord( GuidValue );

        It should_return_column_name_when_getname_is_called
            = () => MockUnderTest.GetName( 0 ).ShouldEqual( ColumnName );

        It should_return_false_when_isdbnull_is_called
            = () => MockUnderTest.IsDBNull( 0 ).ShouldBeFalse();

        It should_return_guid_when_getdatatypename_is_called
            = () => MockUnderTest.GetDataTypeName( 0 ).ShouldEqual( "Guid" );

        It should_return_the_value_when_getvalue_is_called
            = () => MockUnderTest.GetValue( 0 ).ShouldEqual( GuidValue );

        It should_return_typeof_guid_when_getfieldtype_is_called
            = () => MockUnderTest.GetFieldType( 0 ).ShouldEqual( typeof(Guid) );

        It should_return_valid_datareader_when_getdate_is_called
            = () => AssertThatDataReaderFromGetDataIsCorrect( MockUnderTest.GetData( 0 ), GuidValue );

        It should_return_valid_value_in_array_when_getvalues_is_called
            = () => AssertThatArrayFromGetValuesIsCorrect( GuidValue );

        It should_return_value_when_getguid_is_called
            = () => MockUnderTest.GetGuid( 0 ).ShouldEqual( GuidValue );

        It should_return_value_when_getint32_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt32( 0 ) );

        It should_return_value_when_name_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[ColumnName].ShouldEqual( GuidValue );

        It should_return_value_when_ordinal_indexer_is_used
            = () => ((IDataReader)MockUnderTest)[0].ShouldEqual( GuidValue );

        It should_throw_exception_when_getboolean_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetBoolean( 0 ) );

        It should_throw_exception_when_getbyte_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetByte( 0 ) );

        It should_throw_exception_when_getchar_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetChar( 0 ) );

        It should_throw_exception_when_getdatetime_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDateTime( 0 ) );

        It should_throw_exception_when_getdecimal_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDecimal( 0 ) );

        It should_throw_exception_when_getdouble_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetDouble( 0 ) );

        It should_throw_exception_when_getfloat_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetFloat( 0 ) );

        It should_throw_exception_when_getint16_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt16( 0 ) );

        It should_throw_exception_when_getint64_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetInt64( 0 ) );

        It should_throw_exception_when_getstring_is_called
            = () => typeof(InvalidCastException).ShouldBeThrownBy( () => MockUnderTest.GetString( 0 ) );
    }
}