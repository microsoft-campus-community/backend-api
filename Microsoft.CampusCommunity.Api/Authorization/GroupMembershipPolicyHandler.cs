﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CampusCommunity.Infrastructure.Helpers;

namespace Microsoft.CampusCommunity.Api.Authorization
{   
    /// <summary>
    /// Dynamic Policy Handler that decides whether or not a user has the correct group membership to access a resource
    /// </summary>
    public class GroupMembershipPolicyHandler : AuthorizationHandler<GroupMembershipRequirement>
    {
        /// <summary>
        /// Implementation of AuthorizationHandler. Sets context.Succeed in case of met group requirement
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            GroupMembershipRequirement requirement)
        {
            IList<Guid> groups;
            try
            {
                groups = AuthenticationHelper.GetAaDGroups(context.User).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.CompletedTask;
            }

            // does the user have at least one of the necessary group memberships?
            var matches = groups.Intersect(requirement.GroupMemberships).Count();
            if (matches > 0) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}