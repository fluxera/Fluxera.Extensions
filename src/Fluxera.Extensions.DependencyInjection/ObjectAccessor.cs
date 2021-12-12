namespace Fluxera.Extensions.DependencyInjection
{
	using System;

	internal sealed class ObjectAccessor<T> : IObjectAccessor<T> where T : class
	{
		private bool isDisposed;

		private T? value;

		public ObjectAccessor(ObjectAccessorContext? context = null)
		{
			Context = context ?? ObjectAccessorContext.Default;
		}

		public ObjectAccessor(T? value, ObjectAccessorContext? context = null)
			: this(context)
		{
			Value = value;
		}

		public ObjectAccessorContext Context { get; }

		object? IObjectAccessor.Value
		{
			set => Value = value as T;
		}

		public T? Value
		{
			get
			{
				AssureNotDisposed();
				return value;
			}
			set
			{
				AssureNotDisposed();
				this.value = value;
			}
		}

		/// <inheritdoc />
		public void Dispose()
		{
			value = null;
			isDisposed = true;
		}

		private void AssureNotDisposed()
		{
			if (isDisposed)
			{
				throw new ObjectDisposedException($"The object accessor was already disposed. (Context={Context})");
			}
		}
	}
}
