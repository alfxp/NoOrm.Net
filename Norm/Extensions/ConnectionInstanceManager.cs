﻿using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using Norm.Interfaces;

namespace Norm.Extensions
{
    public static partial class ConnectionExtensions
    {
        private static readonly ConditionalWeakTable<DbConnection, Norm> Table;
        static ConnectionExtensions()
        {
            Table = new ConditionalWeakTable<DbConnection, Norm>();
        }

        internal static Norm GetNoOrmInstance(this DbConnection connection)
        {
            if (Table.TryGetValue(connection, out var instance))
            {
                return instance;
            }
            instance = new Norm(connection);
            Table.Add(connection, instance);
            return instance;
        }

        public static INorm As(this DbConnection connection, CommandType type) => 
            connection.GetNoOrmInstance().Clone().As(type);

        public static INorm AsProcedure(this DbConnection connection) => 
            connection.As(CommandType.StoredProcedure);

        public static INorm AsText(this DbConnection connection) => 
            connection.As(CommandType.Text);

        public static INorm Timeout(this DbConnection connection, int? timeout) => 
            connection.GetNoOrmInstance().Clone().Timeout(timeout);

        public static INorm WithJsonOptions(this DbConnection connection, JsonSerializerOptions jsonOptions) => 
            connection.GetNoOrmInstance().Clone().WithJsonOptions(jsonOptions);

        public static INorm WithCancellationToken(this DbConnection connection, CancellationToken cancellationToken) =>
            connection.GetNoOrmInstance().Clone().WithCancellationToken(cancellationToken);
    }
}
