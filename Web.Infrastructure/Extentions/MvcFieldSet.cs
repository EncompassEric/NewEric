namespace Web.Infrastructure.Extentions
{
    using System;
    using System.Web.Mvc;

    public class MvcFieldSet:IDisposable
	{
		private bool _disposed;
		private readonly ViewContext _viewContext;

		public MvcFieldSet(ViewContext viewContext)
		{
			if (viewContext == null)
			{
				throw new ArgumentException("viewContext");
			}

			this._viewContext = viewContext;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				FieldSetExtensions.EndFieldSet(this._viewContext);
			}
		}

		public void EndFieldSet()
		{
			this.Dispose(true);
		}
	}
}



