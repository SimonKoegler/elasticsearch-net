﻿using System;

namespace Nest
{
	public class ObjectAttribute : ElasticsearchCorePropertyAttributeBase, IObjectProperty
	{
		IObjectProperty Self => this;

		DynamicMapping? IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }
		bool? IObjectProperty.IncludeInAll { get; set; }
		IProperties IObjectProperty.Properties { get; set; }

		public DynamicMapping Dynamic { get { return Self.Dynamic.GetValueOrDefault(); } set { Self.Dynamic = value; } }
		public bool Enabled { get { return Self.Enabled.GetValueOrDefault(); } set { Self.Enabled = value; } }
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }

		public ObjectAttribute() : base("object") { }
		protected ObjectAttribute(string typeName) : base(typeName) { }
		protected ObjectAttribute(Type type) : base(type) { }
	}
}
