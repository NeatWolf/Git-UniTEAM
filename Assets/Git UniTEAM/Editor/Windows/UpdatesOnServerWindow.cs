using UnityEngine;
using System.Linq;
using LibGit2Sharp;

namespace UniTEAM {
	public class UpdatesOnServerWindow {
		private static Vector2 scroll;
		public static Rect rect;

		public static void draw( Console console, int id ) {
			scroll = GUILayout.BeginScrollView( scroll );

			
			foreach ( Commit commit in console.repo.Commits.QueryBy( new Filter {
				Since = console.branch.TrackedBranch, Until = console.branch.Tip
			} ) ) {
				console.getUpdateItem( commit, commit.Parents.First(), rect, onCommitSelected );
			}
			

			GUILayout.EndScrollView();
		}

		private static void onCommitSelected( Commit commit ) {}
	}
}
