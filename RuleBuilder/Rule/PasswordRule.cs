﻿using System.Collections.Generic;

namespace RuleBuilder.Rule {
	internal class PasswordRule : IPasswordGenerator {
		public PasswordRule() { }
		public string NewPassword() {
			List<string> password = new List<string>();
			foreach (Component component in this.Components) {
				HashSet<string> chars = new HashSet<string>(component.CharacterClass.CharacterSet);
				foreach (string c in this.ExcludeChars) {
					_ = chars.Remove(c);
				}
				if (chars.Count > 0) {
					for (int count = 0; count < component.MinCount; count++) {
						password.Add(Random.RandomItem(chars));
					}
				}
			}
			this.FillToLength(password);
			Random.Shuffle(password);
			return string.Join(string.Empty, password);
		}
		private void FillToLength(List<string> password) {
			if (password.Count >= this.Length) {
				return;
			}
			HashSet<string> allCharacters = this.AllCharacters();
			if (allCharacters.Count > 0) {
				while (password.Count < this.Length) {
					password.Add(Random.RandomItem(allCharacters));
				}
			}
			return;
		}
		public int Length { get; set; } = (int)KeePass.Program.Config.PasswordGenerator.AutoGeneratedPasswordsProfile.Length;
		public string Exclude {
			get => CharacterClass.SortedString(this.ExcludeChars);
			set => this.ExcludeChars = CharacterClass.SplitString(value);
		}
		public HashSet<string> ExcludeChars { get; private set; } = new HashSet<string>();
		public List<Component> Components { get; set; } = new List<Component>();
		private HashSet<string> AllCharacters() {
			HashSet<string> chars = new HashSet<string>();
			foreach (Component component in this.Components) {
				chars.UnionWith(component.CharacterClass.CharacterSet);
			}
			foreach (string c in this.ExcludeChars) {
				_ = chars.Remove(c);
			}
			return chars;
		}
	}
}