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

using FluentValidation.Validators;
using System;
using Xunit;

namespace FluentValidation.Tests {
  public class GuidValidatorTests {
    public GuidValidatorTests() {
      CultureScope.SetDefaultCulture();
    }

    [Fact]
    public void Is_Guid_Valid() {
      var validator = new GuidValidator();
      var result = validator.Validate(new Car(Guid.NewGuid().ToString()));
      Assert.True(result.IsValid);
    }

    [Fact]
    public void Allow_Guid_Empty_Valid() {
      var validator = new GuidValidator();
      var result = validator.Validate(new Car(Guid.Empty.ToString()));
      Assert.True(result.IsValid);
    }

    [Fact]
    public void Do_Not_Allow_Guid_Empty_InValid() {
      var validator = new GuidValidator(false);
      var result = validator.Validate(new Car(Guid.Empty.ToString()));
      Assert.False(result.IsValid);
    }

    [Fact]
    public void Do_Not_Allow_Guid_null_InValid() {
      var validator = new GuidValidator(false);
      var result = validator.Validate(new Car(null));
      Assert.False(result.IsValid);
    }

    [Fact]
    public void Allow_Guid_null_Valid() {
      var validator = new GuidValidator();
      var result = validator.Validate(new Car(null));
      Assert.True(result.IsValid);
    }
  }

  class Car {
    public string Id { get; set; }

    public Car(string id) {
      Id = id;
    }
  }

  class GuidValidator : AbstractValidator<Car> {
    public GuidValidator(bool allowEmptyGuid = true) {
      RuleFor(x => x.Id).IsGuid(allowEmptyGuid);
    }
  }
}
