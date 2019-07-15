﻿using System.Runtime.Serialization;

namespace RuleBuilder.Rule.Serialization {
	[DataContract]
	internal class ProfileContract {
		public ProfileContract(PasswordProfile profile) {
			this.IsDefault = profile.IsDefaultProfile;
			if (!this.IsDefault) {
				this.Name = profile.Name;
			}
		}
		public PasswordProfile Object() => this.IsDefault ? PasswordProfile.DefaultProfile : PasswordProfile.NamedProfile(this.Name);
		[DataMember(EmitDefaultValue = false)]
		public bool IsDefault { get; private set; }
		[DataMember(EmitDefaultValue = false)]
		public string Name { get; private set; }
	}
}
