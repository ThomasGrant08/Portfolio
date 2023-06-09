//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v11.4.0+e52e987
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	// Mixin Content Type with alias "visibilityOFF"
	/// <summary>Visibility OFF</summary>
	public partial interface IVisibilityOff : IPublishedElement
	{
		/// <summary>Show On Footer</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		bool ShowOnFooter { get; }

		/// <summary>Visibility</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		bool Visibility { get; }
	}

	/// <summary>Visibility OFF</summary>
	[PublishedModel("visibilityOFF")]
	public partial class VisibilityOff : PublishedElementModel, IVisibilityOff
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		public new const string ModelTypeAlias = "visibilityOFF";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<VisibilityOff, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public VisibilityOff(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Show On Footer
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		[ImplementPropertyType("showOnFooter")]
		public virtual bool ShowOnFooter => GetShowOnFooter(this, _publishedValueFallback);

		/// <summary>Static getter for Show On Footer</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		public static bool GetShowOnFooter(IVisibilityOff that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "showOnFooter");

		///<summary>
		/// Visibility
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		[ImplementPropertyType("visibility")]
		public virtual bool Visibility => GetVisibility(this, _publishedValueFallback);

		/// <summary>Static getter for Visibility</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "11.4.0+e52e987")]
		public static bool GetVisibility(IVisibilityOff that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "visibility");
	}
}
