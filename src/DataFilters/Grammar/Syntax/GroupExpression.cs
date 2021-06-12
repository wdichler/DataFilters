﻿using System;

namespace DataFilters.Grammar.Syntax
{
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
#if NETSTANDARD1_3
    public sealed class GroupExpression : FilterExpression, IEquatable<GroupExpression>
#else
    public record GroupExpression : FilterExpression, IEquatable<GroupExpression>
#endif
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

#if NETSTANDARD1_3
        ///<inheritdoc/>
        public bool Equals(GroupExpression other) => Expression.Equals(other?.Expression);

        ///<inheritdoc/>
        public override bool Equals(object obj) => Equals(obj as GroupExpression);

        ///<inheritdoc/>
        public override int GetHashCode() => Expression.GetHashCode();

        ///<inheritdoc/>
        public override string ToString() => $"{GetType().Name} : Expression ({Expression.GetType().Name }) -> {Expression}";
#endif

        ///<inheritdoc/>
        public override double Complexity => Expression.Complexity;

        ///<inheritdoc/>
        public override bool IsEquivalentTo(FilterExpression other) => Expression.IsEquivalentTo(other);
    }
}