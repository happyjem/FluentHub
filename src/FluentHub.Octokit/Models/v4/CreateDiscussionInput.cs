namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of CreateDiscussion
    /// </summary>
    public class CreateDiscussionInput
    {        /// <summary>
        /// The id of the repository on which to create the discussion.
        /// </summary>
        public ID RepositoryId { get; set; }

        /// <summary>
        /// The title of the discussion.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The body of the discussion.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The id of the discussion category to associate with this discussion.
        /// </summary>
        public ID CategoryId { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}