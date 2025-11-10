class Activity < ApplicationRecord
  belongs_to :business
  has_many :attendance_records
  has_many :students, through: :attendance_records
  has_many :qr_codes
end
