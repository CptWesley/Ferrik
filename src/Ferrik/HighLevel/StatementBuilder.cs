using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Ferrik.HighLevel.StatementTypes;

namespace Ferrik.HighLevel
{
    /// <summary>
    /// A helper class for constructing block statements.
    /// </summary>
    public class StatementBuilder : Statement
    {
        private readonly List<Statement> statements = new List<Statement>();

        /// <summary>
        /// Adds a statement.
        /// </summary>
        /// <param name="statement">The new statement.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder Add(Statement statement)
        {
            statements.Add(statement);
            return this;
        }

        /// <inheritdoc/>
        public override void Emit(ILGenerator il, Scope scope)
            => ToStatement().Emit(il, scope);

        /// <summary>
        /// Turns the builder into a block statement.
        /// </summary>
        /// <returns>The new block statement.</returns>
        public Statement ToStatement()
            => new BlockStatement(statements);

        /// <summary>
        /// Adds a variable declaration statement.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="type">The type of the variable.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder Declare(string name, Type type)
            => Add(Statements.Declare(name, type));

        /// <summary>
        /// Adds a variable assignment statement.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="expression">The expression of the new value of the variable.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder Assign(string name, Expression expression)
            => Add(Statements.Assign(name, expression));

        /// <summary>
        /// Adds a variable assignment statement.
        /// </summary>
        /// <param name="index">The index of the argument variable.</param>
        /// <param name="expression">The expression of the new value of the variable.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder Assign(int index, Expression expression)
            => Add(Statements.Assign(index, expression));

        /// <summary>
        /// Adds a return statement.
        /// </summary>
        /// <param name="expression">The expression representing the value to return.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder Return(Expression expression)
            => Add(Statements.Return(expression));

        /// <summary>
        /// Adds a new block statement.
        /// </summary>
        /// <param name="statements">The statements in the block statement.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder Block(IEnumerable<Statement> statements)
            => Add(Statements.Block(statements));

        /// <summary>
        /// Adds a new block statement.
        /// </summary>
        /// <param name="statements">The statements in the block statement.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder Block(params Statement[] statements)
            => Block(statements as IEnumerable<Statement>);

        /// <summary>
        /// Adds a new block statement.
        /// </summary>
        /// <param name="action">The action to construct a new statement builder.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder Block(Func<StatementBuilder, StatementBuilder> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Add(action(new StatementBuilder()));
        }

        /// <summary>
        /// Adds a new if statement.
        /// </summary>
        /// <param name="condition">The condition expression that must hold.</param>
        /// <param name="then">The body of the if statement.</param>
        /// <returns>The same builder.</returns>
        public StatementBuilder If(Expression condition, Statement then)
            => Add(Statements.If(condition, then));
    }
}
