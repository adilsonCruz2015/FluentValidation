#region License

// Copyright (c) Jeremy Skinner (http://www.jeremyskinner.co.uk)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at https://github.com/jeremyskinner/FluentValidation
#endregion

using FluentValidation.Resources;
using System;
using System.Text.RegularExpressions;

namespace FluentValidation.Validators {
  public class GuidValidator : PropertyValidator {

    private readonly Regex _regex;
    private const string _expression = @"^[a-z0-9]{8}(-[a-z0-9]{4}){3}-[a-z0-9]{12}$";
    private readonly bool _allowEmptyGuid;    

    public GuidValidator(bool allowEmptyGuid) : base(new LanguageStringSource(nameof(GuidValidator))) {
      _regex = new Regex(_expression, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2.0));
      _allowEmptyGuid = !allowEmptyGuid;
    }

    protected override bool IsValid(PropertyValidatorContext context) {
      if (_allowEmptyGuid.Equals(true) && context.PropertyValue == null) return false;
      if(context.PropertyValue == null) return true;

      string propertyValue = context.PropertyValue.ToString();
      if (_regex.IsMatch(propertyValue)) {
        if (_allowEmptyGuid && propertyValue.Equals(Guid.Empty.ToString()))
          return false;
      }
      else {
        return false;
      }
      return true;    
    }
  }
}
