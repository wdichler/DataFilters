﻿namespace DataFilters.Grammar.Syntax
{
    using System;

    /// <summary>
    /// Allows to treat several expressions as a single unit.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A group expression can be used whenever there is a need to apply a logical operator to several expressions at once.
    /// </para>
    /// <para>
    /// The <see cref="Complexity"/> value of a <see cref="GroupExpression"/> is equivalent to the complexity of its inner <see cref="Expression"/>.
    /// </para>
    /// </remarks>
    public sealed class GroupExpression : FilterExpression, IEquatable<GroupExpression>, ISimplifiable
    {
        /// <summary>
        /// Expression that the group is applied onto
        /// </summary>
        public FilterExpression Expression { get; }

        /// <summary>
        /// Builds a new <see cref="GroupExpression"/> that holds the specified <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="ArgumentNullException"><paramref name="expression"/> is <c>null</c>.</exception>
        public GroupExpression(FilterExpression expression) => Expression = expression ?? throw new ArgumentNullException(nameof(expression));

        ///<inheritdoc/>
        public bool Equals(GroupExpression other) => Expression.Equals(other?.Expression);

        ///<inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as GroupExpression);

        ///<inheritdoc/>
        public override int GetHashCode() => Expression.GetHashCode();

        ///<inheritdoc/>
        public override string ToString() => $"{{GroupExpression " +
            $"[Expression = {Expression.GetType().Name}, " +
            $"{nameof(Expression.EscapedParseableString)} = '{Expression.EscapedParseableString}', " +
            $"{nameof(Expression.OriginalString)} = '{Expression.OriginalString}'], " +
            $"{nameof(EscapedParseableString)}='{EscapedParseableString}'}}";

        ///<inheritdoc/>
        public override string EscapedParseableString => $"({Expression.EscapedParseableString})";

        ///<inheritdoc/>
        public override double Complexity => Expression.Complexity;

        ///<inheritdoc/>
        public override bool IsEquivalentTo(FilterExpression other) => other switch
        {
            GroupExpression group => Expression.IsEquivalentTo(group.Expression),
            _ => Expression.IsEquivalentTo(other)
        };

        ///<inheritdoc/>
        public FilterExpression Simplify()
            => Expression switch
            {
                ConstantValueExpression constant => constant,
                ISimplifiable simplify => simplify.Simplify(),
                _ => Expression
            };
    }
}