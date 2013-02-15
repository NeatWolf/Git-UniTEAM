using System;
using UnityEngine;
using LibGit2Sharp;
using System.Linq;

namespace UniTEAM {
	public class LocalStashedCommitsWindow {
		public static Rect rect;
		private static Vector2 scroll = Vector2.zero;
		public static bool isPushing = false;
		private static bool doesRequireFetch = false;

		public static void draw( Console console, int i ) {
			scroll = GUILayout.BeginScrollView( scroll );

			GUI.enabled = !isPushing;

			if ( isPushing ) {
				doesRequireFetch = true;
			} else if ( !isPushing && doesRequireFetch ) {
				//# Trigger a fetch
				doesRequireFetch = false;
				console.fetch();
			}

			foreach ( Commit commit in console.repo.Commits.QueryBy( new Filter {
				Since = console.branch.Tip, Until = console.branch.TrackedBranch
			} ) ) {
				console.getUpdateItem( commit, commit.Parents.First(), rect, onCommitSelected );
			}

			GUI.enabled = true;

			GUILayout.EndScrollView();
		}

		private static void onCommitSelected( Commit commit ) {}
	}
}
