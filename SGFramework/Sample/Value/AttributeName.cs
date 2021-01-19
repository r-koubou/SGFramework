using System;

namespace SGFramework.Sample.Value
{
    public class AttributeName : IEquatable<AttributeName>
    {
        public string Value { get; }

        public AttributeName( string value )
        {
            Value = value;
        }

        public bool Equals( AttributeName? other )
        {
            if( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return Value == other.Value;
        }

        public override bool Equals( object? obj )
        {
            if( ReferenceEquals( null, obj ) )
            {
                return false;
            }

            if( ReferenceEquals( this, obj ) )
            {
                return true;
            }

            if( obj.GetType() != this.GetType() )
            {
                return false;
            }

            return Equals( (AttributeName)obj );
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==( AttributeName? left, AttributeName? right ) => Equals( left, right );

        public static bool operator !=( AttributeName? left, AttributeName? right ) => !Equals( left, right );
    }
}
