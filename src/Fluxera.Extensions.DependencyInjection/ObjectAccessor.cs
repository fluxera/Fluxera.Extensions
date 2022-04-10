namespace Fluxera.Extensions.DependencyInjection
{
	using System;

	internal sealed class ObjectAccessor<T> : IObjectAccessor<T> where T : class
	{
		private bool isDisposed;

		private T value;

		public ObjectAccessor(ObjectAccessorContext context = null)
		{
			this.Context = context ?? ObjectAccessorContext.Default;
		}

		public ObjectAccessor(T value, ObjectAccessorContext context = null)
			: this(context)
		{
			this.Value = value;
		}

		public ObjectAccessorContext Context { get; }

		object IObjectAccessor.Value
		{
			set => this.Value = value as T;
		}

		public T Value
		{
			get
			{
				this.AssureNotDisposed();
				return this.value;
			}
			set
			{
				this.AssureNotDisposed();
				this.value = value;
			}
		}

		/// <inheritdoc />
		public void Dispose()
		{
			this.value = null;
			this.isDisposed = true;
		}

		private void AssureNotDisposed()
		{
			if(this.isDisposed)
			{
				throw new ObjectDisposedException($"The object accessor was already disposed. (Context={this.Context})");
			}
		}
	}
}
