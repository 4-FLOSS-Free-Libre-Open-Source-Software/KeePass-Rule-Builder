﻿using System.Runtime.Serialization;

namespace RuleBuilder.Rule.Serialization {
	[DataContract]
	internal class ComponentContract {
		public ComponentContract(Component component) {
			this.MinCount = component.MinCount;
			this.CharacterSet = new CharacterClassContract(component.CharacterClass);
		}
		public Component Object() {
			if (this.MinCount < 0) {
				throw new SerializationException("Minimum count must be a positive number.");
			}
			return new Component(this.CharacterSet.Object(), this.MinCount);
		}
		[DataMember]
		public int MinCount { get; private set; }
		[DataMember]
		public CharacterClassContract CharacterSet { get; private set; }
	}
}
