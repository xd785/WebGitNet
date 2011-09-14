﻿namespace WebGitNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class IgnoreFileParser
    {
        public static List<IgnoreEntry> Parse(string[] lines)
        {
            var entries = new List<IgnoreEntry>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                {
                    continue;
                }

                var lineParts = line.Split(":".ToArray(), 2);

                var commitHash = lineParts[0];
                var pathGlobs = lineParts[1];
                var negated = false;
                var rooted = false;

                if (pathGlobs.StartsWith("!"))
                {
                    negated = true;
                    pathGlobs = pathGlobs.Substring(1);
                }

                if (pathGlobs.StartsWith("/"))
                {
                    rooted = true;
                    pathGlobs = pathGlobs.Substring(1);
                }

                if (pathGlobs.EndsWith("/"))
                {
                    pathGlobs += "*";
                }

                entries.Add(new IgnoreEntry { CommitHash = commitHash, PathGlobs = pathGlobs, Negated = negated, Rooted = rooted });
            }

            return entries;
        }
    }
}