﻿using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Norm.Extensions
{
    public static partial class ConnectionExtensions
    {
        public static IEnumerable<T> Json<T>(this DbConnection connection, string command) =>
            connection.GetNoOrmInstance().Json<T>(command);

        public static IEnumerable<T> Json<T>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetNoOrmInstance().Json<T>(command, parameters);

        public static IEnumerable<T> Json<T>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetNoOrmInstance().Json<T>(command, parameters);

        public static IEnumerable<T> Json<T>(this DbConnection connection, string command, params (string name, object value, DbType type)[] parameters) =>
            connection.GetNoOrmInstance().Json<T>(command, parameters);
    }
}

