class Business < ApplicationRecord
  validates :name, presence: true
  has_many :employees
  has_many :activities
end
