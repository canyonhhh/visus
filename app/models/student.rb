class Student < ApplicationRecord
  validates :full_name, presence: true
  has_many :attendance_records
  has_many :activities, through: :attendance_records
  has_many :device_fingerprints
end
