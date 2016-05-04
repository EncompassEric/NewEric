namespace Web.Infrastructure.Extentions
{
    using System.Web.Mvc;

    public static class FieldSetExtensions
	{
		public static MvcFieldSet BeginFieldSet(this HtmlHelper htmlHelper,string title)
		{
			TagBuilder builder=new TagBuilder("fieldset");

			htmlHelper.ViewContext.Writer.Write(builder.ToString(TagRenderMode.StartTag));
			if (!string.IsNullOrWhiteSpace(title))
			{
				TagBuilder legend=new TagBuilder("legend");
				legend.SetInnerText(title);

				htmlHelper.ViewContext.Writer.Write(legend.ToString());
			}

			MvcFieldSet fieldSet=new MvcFieldSet(htmlHelper.ViewContext);

			return fieldSet;
		}

		public static void EndFieldSet(this HtmlHelper htmlHelper)
		{
			EndFieldSet(htmlHelper.ViewContext);
		}

		internal static void EndFieldSet(ViewContext viewContext)
		{
			viewContext.Writer.Write("</fieldset>");
		}
	}
}
