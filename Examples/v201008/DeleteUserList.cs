// Copyright 2010, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using com.google.api.adwords.lib;
using com.google.api.adwords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v201008 {
  /// <summary>
  /// This code example deletes a user list by setting the status to 'CLOSED'.
  /// To get user lists, run GetAllUserLists.cs.
  ///
  /// Tags: UserListService.mutate
  /// </summary>
  class DeleteUserList : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes a user list by setting the status to 'CLOSED'. To " +
            "get user lists, run GetAllUserLists.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the UserListService.
      UserListService userListService =
          (UserListService) user.GetService(AdWordsService.v201008.UserListService);

      long userListId = int.Parse(_T("INSERT_USER_LIST_ID_HERE"));

      // Prepare for deleting remarketing user list. Bear in mind that you must
      // create an object of the appropriate type in order to perform the
      // update. If you are unsure which type a user list is, you should perform
      // a 'get' on it first.
      RemarketingUserList userList = new RemarketingUserList();
      userList.id = userListId;
      userList.status = UserListMembershipStatus.CLOSED;

      UserListOperation operation = new UserListOperation();
      operation.operand = userList;
      operation.@operator = Operator.SET;

      try {
        // Delete user list.
        UserListReturnValue retval = userListService.mutate(new UserListOperation[] {operation});
        if (retval != null && retval.value != null & retval.value.Length > 0) {
          UserList tempUserList = retval.value[0];
          Console.WriteLine("User list with name \"{0}\" and id {1} was deleted (closed).",
              tempUserList.name, tempUserList.id);
        } else {
          Console.WriteLine("No user lists were deleted (closed).");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete (close) user lists. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
