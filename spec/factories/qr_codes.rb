FactoryBot.define do
  factory :qr_code do
    activity { nil }
    token { "MyString" }
    is_active { false }
  end
end
