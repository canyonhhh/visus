FactoryBot.define do
  factory :employee do
    business { nil }
    name { "MyString" }
    email { "MyString" }
    role { "MyString" }
    password_digest { "MyString" }
  end
end
