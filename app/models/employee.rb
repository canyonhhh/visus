class Employee < ApplicationRecord
  belongs_to :business
  has_secure_password

  enum :role, { staff: 0, admin: 1 }
end
