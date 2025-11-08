FactoryBot.define do
  factory :activity do
    business { nil }
    name { "MyString" }
    description { "MyText" }
    is_active { false }
  end
end
