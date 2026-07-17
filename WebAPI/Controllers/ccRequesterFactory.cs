using System;
using System.Security.Claims;
using TargCCOrders.DataController;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// Builds a REAL clsRequester for the authenticated user from the TargCC
    /// ticket embedded in the JWT ("tccTicket" claim).
    ///
    /// This replaces the previous pattern of
    ///     new clsRequester(userName, "View", true)
    /// which invoked the SecurityExempt constructor — bypassing the entire
    /// TargCC permission model and attributing every audit row to
    /// "SecurityExempt" instead of the real user.
    /// </summary>
    public static class RequesterFactory
    {
        public const string TicketClaim = "tccTicket";

        /// <summary>
        /// Reconstructs the requester from the JWT ticket claim.
        /// Throws InvalidOperationException when the ticket is missing/invalid —
        /// controllers translate that into 401.
        /// </summary>
        public static clsRequester FromUser(ClaimsPrincipal user)
        {
            var ticket = user?.FindFirst(TicketClaim)?.Value;
            if (string.IsNullOrEmpty(ticket))
                throw new InvalidOperationException("Missing TargCC ticket in token. Please log in again.");

            // Ticket constructor — decrypts (TripleDES, DBController key) and loads
            // the full identity (UserID, roles, identity type) without a DB round trip.
            var requester = new clsRequester(ticket);
            requester.CallingFunctionWithinApplication = "WebAPI";
            return requester;
        }
    }
}
