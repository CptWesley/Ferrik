using System;
using System.Collections.Generic;
using Ferrik.HighLevel;
using Ferrik.HighLevel.StatementTypes;

namespace Ferrik
{
    /// <summary>
    /// Defines an easy to use API for dealing with statements.
    /// </summary>
    public static class Statements
    {
        /// <summary>
        /// Creates a new assign statement.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value to be assigned to the variable.</param>
        /// <returns>The new statement.</returns>
        public static Statement Assign(string name, Expression value)
            => new AssignStatement(name, value);

        /// <summary>
        /// Creates a new assign statement.
        /// </summary>
        /// <param name="index">The index of the argument variable.</param>
        /// <param name="value">The value to be assigned to the variable.</param>
        /// <returns>The new statement.</returns>
        public static Statement Assign(int index, Expression value)
            => new AssignStatement("@" + index, value);

        /// <summary>
        /// Creates a new block statement.
        /// </summary>
        /// <param name="statements">The statements in the block.</param>
        /// <returns>The new statement.</returns>
        public static Statement Block(IEnumerable<Statement> statements)
            => new BlockStatement(statements);

        /// <summary>
        /// Creates a new statement builder.
        /// </summary>
        /// <returns>The new statement builder.</returns>
        public static StatementBuilder Builder()
            => new StatementBuilder();

        /// <summary>
        /// Creates a new declaration statement.
        /// </summary>
        /// <param name="name">The name of the new variable.</param>
        /// <param name="type">The type of the new variable.</param>
        /// <returns>The new statement.</returns>
        public static Statement Declare(string name, Type type)
            => new DeclareStatement(name, type);

        /// <summary>
        /// Creates a new return statement.
        /// </summary>
        /// <param name="expression">The expression to be returned.</param>
        /// <returns>The new statement.</returns>
        public static Statement Return(Expression expression)
            => new ReturnStatement(expression);

        /// <summary>
        /// Creates a new if statement.
        /// </summary>
        /// <param name="condition">The condition expression that must hold.</param>
        /// <param name="then">The body of the if statement.</param>
        /// <returns>The new statement.</returns>
        public static Statement If(Expression condition, Statement then)
            => new IfStatement(condition, then);
    }
}
