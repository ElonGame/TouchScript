/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System.Collections.Generic;
using TouchScript.Hit;
using TouchScript.Layers;
using UnityEngine;

namespace TouchScript
{
    /// <summary>
    /// Class which internally represents a touch.
    /// </summary>
    internal sealed class TouchPoint : ITouch
    {
        #region Public properties

        /// <inheritdoc />
        public int Id { get; private set; }

        /// <inheritdoc />
        public Transform Target { get; internal set; }

        /// <inheritdoc />
        public Vector2 Position
        {
            get { return position; }
        }

        /// <inheritdoc />
        public Vector2 PreviousPosition { get; private set; }

        /// <inheritdoc />
        public TouchHit Hit { get; internal set; }

        /// <inheritdoc />
        public TouchLayer Layer { get; internal set; }

        /// <inheritdoc />
        public ProjectionParams ProjectionParams
        {
            get { return Layer.GetProjectionParams(this); }
        }

        /// <inheritdoc />
        public Tags Tags { get; private set; }

        /// <inheritdoc />
        public IDictionary<string, object> Properties
        {
            get { return properties; }
        }

        #endregion

        #region Private variables

        private Vector2 position = Vector2.zero;
        private Vector2 newPosition = Vector2.zero;
        private Dictionary<string, object> properties;

        #endregion

        #region Public methods

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            return Equals(other as ITouch);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Id;
        }

        /// <inheritdoc />
        public bool Equals(ITouch other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }

        #endregion

        public TouchPoint()
        {
            properties = new Dictionary<string, object>();
        }

        #region Internal methods

        internal void INTERNAL_Reset()
        {
            Hit = default(TouchHit);
            Target = null;
            Layer = null;
            Tags = null;
            properties.Clear();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchPoint"/> class.
        /// </summary>
        /// <param name="id">Unique id of the touch.</param>
        /// <param name="position">Screen position of the touch.</param>
        /// <param name="tags">Initial tags.</param>
        internal void INTERNAL_Init(int id, Vector2 position, Tags tags)
        {
            Id = id;
            this.position = PreviousPosition = newPosition = position;
            Tags = tags ?? Tags.EMPTY;
        }

        /// <summary>
        /// </summary>
        internal void INTERNAL_ResetPosition()
        {
            PreviousPosition = position;
            position = newPosition;
            newPosition = position;
        }

        internal void INTERNAL_SetPosition(Vector2 value)
        {
            newPosition = value;
        }

        #endregion
    }
}