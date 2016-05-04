namespace Application.Seedwork
{
    using System;
    using System.Collections.Generic;

    public class ApplicationValidationErrorsException
		: Exception
	{
		#region Properties

		IEnumerable<string> _validationErrors;
		/// <summary>
		/// Get or set the validation errors messages
		/// </summary>
		public IEnumerable<string> ValidationErrors
		{
			get
			{
				return _validationErrors;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Create new instance of Application validation errors exception
		/// </summary>
		/// <param name="validationErrors">The collection of validation errors</param>
		public ApplicationValidationErrorsException(IEnumerable<string> validationErrors)
			: base("Validation exception, check ValidationErrors for more information")
		{
			_validationErrors = validationErrors;
		}

		#endregion
	}
}
